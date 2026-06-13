using Microsoft.EntityFrameworkCore;
using Timetable_and_Classroom_Management_System.DataAccessLayer;
using Timetable_and_Classroom_Management_System.Models;

namespace Timetable_and_Classroom_Management_System.BusinessLayer
{
    public class ScheduleService
    {
        private static readonly HashSet<string> ValidDays = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Sunday",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday"
        };

        private static readonly string[] TeachingDays =
        {
            "Sunday",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday"
        };

        public List<Schedule> GetAllSchedules()
        {
            using AppDbContext context = new AppDbContext();

            return context.Schedules
                .Include(s => s.Subject)
                .Include(s => s.FacultyMember)
                .Include(s => s.Classroom)
                .Include(s => s.TimeSlot)
                .Include(s => s.StudyYear)
                .Include(s => s.Branch)
                .Include(s => s.Section)
                .ToList()
                .OrderBy(s => GetDayOrder(s.DayOfWeek))
                .ThenBy(s => s.TimeSlot.StartTime)
                .ToList();
        }

        public void AddSchedule(
            int subjectId,
            int facultyMemberId,
            int classroomId,
            int timeSlotId,
            string dayOfWeek,
            int? studyYearId,
            int? branchId,
            int? sectionId)
        {
            dayOfWeek = NormalizeDay(dayOfWeek);

            using AppDbContext context = new AppDbContext();

            ValidateScheduleInput(context, subjectId, facultyMemberId, classroomId, timeSlotId, dayOfWeek, studyYearId, branchId, sectionId);
            EnsureNoScheduleConflicts(context, 0, facultyMemberId, classroomId, timeSlotId, dayOfWeek, sectionId);

            Schedule schedule = new Schedule
            {
                SubjectID = subjectId,
                FacultyMemberID = facultyMemberId,
                ClassroomID = classroomId,
                TimeSlotID = timeSlotId,
                DayOfWeek = dayOfWeek,
                StudyYearID = studyYearId,
                BranchID = branchId,
                SectionID = sectionId
            };

            context.Schedules.Add(schedule);
            context.SaveChanges();
        }

        public void UpdateSchedule(
            int scheduleId,
            int subjectId,
            int facultyMemberId,
            int classroomId,
            int timeSlotId,
            string dayOfWeek,
            int? studyYearId,
            int? branchId,
            int? sectionId)
        {
            if (scheduleId <= 0)
            {
                throw new Exception("Please select a schedule entry to update.");
            }

            dayOfWeek = NormalizeDay(dayOfWeek);

            using AppDbContext context = new AppDbContext();

            Schedule? schedule = context.Schedules
                .FirstOrDefault(s => s.ScheduleID == scheduleId);

            if (schedule == null)
            {
                throw new Exception("Schedule entry not found.");
            }

            ValidateScheduleInput(context, subjectId, facultyMemberId, classroomId, timeSlotId, dayOfWeek, studyYearId, branchId, sectionId);
            EnsureNoScheduleConflicts(context, scheduleId, facultyMemberId, classroomId, timeSlotId, dayOfWeek, sectionId);

            schedule.SubjectID = subjectId;
            schedule.FacultyMemberID = facultyMemberId;
            schedule.ClassroomID = classroomId;
            schedule.TimeSlotID = timeSlotId;
            schedule.DayOfWeek = dayOfWeek;
            schedule.StudyYearID = studyYearId;
            schedule.BranchID = branchId;
            schedule.SectionID = sectionId;

            context.SaveChanges();
        }

        public void DeleteSchedule(int scheduleId)
        {
            if (scheduleId <= 0)
            {
                throw new Exception("Please select a schedule entry to delete.");
            }

            using AppDbContext context = new AppDbContext();

            Schedule? schedule = context.Schedules
                .FirstOrDefault(s => s.ScheduleID == scheduleId);

            if (schedule == null)
            {
                throw new Exception("Schedule entry not found.");
            }

            context.Schedules.Remove(schedule);
            context.SaveChanges();
        }

        public ScheduleGenerationResult GenerateAutomaticSchedule(bool replaceExistingSchedules, int? semesterNumber = null)
        {
            using AppDbContext context = new AppDbContext();

            IQueryable<Subject> subjectQuery = context.Subjects;

            if (semesterNumber.HasValue)
            {
                subjectQuery = subjectQuery.Where(s => s.SemesterNumber == semesterNumber.Value);
            }

            List<Subject> subjects = subjectQuery
                .OrderBy(s => s.StudyYearID)
                .ThenBy(s => s.BranchID)
                .ThenBy(s => s.SubjectName)
                .ToList();
            List<Section> sections = context.Sections
                .OrderBy(s => s.StudyYearID)
                .ThenBy(s => s.BranchID)
                .ThenBy(s => s.SectionName)
                .ToList();
            List<Classroom> classrooms = context.Classrooms
                .OrderBy(c => c.Capacity)
                .ThenBy(c => c.ClassroomNumber)
                .ToList();
            List<TimeSlot> timeSlots = context.TimeSlots
                .Where(t => !t.IsBreak)
                .OrderBy(t => t.StartTime)
                .ToList();
            List<FacultyMemberSubject> assignments = context.FacultyMemberSubjects
                .ToList();

            ValidateGenerationData(subjects, sections, classrooms, timeSlots, assignments, semesterNumber);

            using var transaction = context.Database.BeginTransaction();

            if (replaceExistingSchedules)
            {
                context.Schedules.RemoveRange(context.Schedules);
            }

            List<Schedule> allSchedules = replaceExistingSchedules
                ? new List<Schedule>()
                : context.Schedules.AsNoTracking().ToList();

            Dictionary<int, int> facultyLoad = BuildLoadMap(allSchedules, s => s.FacultyMemberID);
            Dictionary<int, int> classroomLoad = BuildLoadMap(allSchedules, s => s.ClassroomID);

            int createdCount = 0;
            int skippedCount = 0;
            List<string> warnings = new List<string>();

            foreach (Section section in sections)
            {
                List<Subject> matchingSubjects = subjects
                    .Where(subject => SubjectMatchesSection(subject, section))
                    .ToList();

                if (matchingSubjects.Count == 0)
                {
                    warnings.Add($"No subjects found for section {section.SectionName}.");
                    continue;
                }

                foreach (Subject subject in matchingSubjects)
                {
                    int requiredSessions = GetRequiredSessionCount(subject);
                    int alreadyScheduled = allSchedules.Count(s => s.SubjectID == subject.SubjectID && s.SectionID == section.SectionID);
                    int remainingSessions = Math.Max(0, requiredSessions - alreadyScheduled);

                    int skippedForSubject = 0;

                    for (int sessionIndex = 0; sessionIndex < remainingSessions; sessionIndex++)
                    {
                        Schedule? schedule = CreateBestScheduleEntry(
                            subject,
                            section,
                            classrooms,
                            timeSlots,
                            assignments,
                            allSchedules,
                            facultyLoad,
                            classroomLoad);

                        if (schedule == null)
                        {
                            skippedCount++;
                            skippedForSubject++;
                            continue;
                        }

                        context.Schedules.Add(schedule);
                        allSchedules.Add(schedule);
                        IncreaseLoad(facultyLoad, schedule.FacultyMemberID);
                        IncreaseLoad(classroomLoad, schedule.ClassroomID);

                        createdCount++;
                    }

                    if (skippedForSubject > 0)
                    {
                        string sessionLabel = skippedForSubject == 1 ? "session" : "sessions";
                        warnings.Add($"Could not place {skippedForSubject} {sessionLabel} of {subject.SubjectName} for section {section.SectionName}.");
                    }
                }
            }

            context.SaveChanges();
            transaction.Commit();

            return new ScheduleGenerationResult(createdCount, skippedCount, warnings, semesterNumber);
        }

        private static void ValidateGenerationData(
            List<Subject> subjects,
            List<Section> sections,
            List<Classroom> classrooms,
            List<TimeSlot> timeSlots,
            List<FacultyMemberSubject> assignments,
            int? semesterNumber)
        {
            if (sections.Count == 0)
            {
                throw new Exception("Add sections before generating a schedule.");
            }

            if (subjects.Count == 0)
            {
                string suffix = semesterNumber.HasValue ? $" for semester {semesterNumber.Value}" : string.Empty;
                throw new Exception($"Add subjects{suffix} before generating a schedule.");
            }

            if (classrooms.Count == 0)
            {
                throw new Exception("Add classrooms before generating a schedule.");
            }

            if (timeSlots.Count == 0)
            {
                throw new Exception("Add at least one non-break time slot before generating a schedule.");
            }

            if (assignments.Count == 0)
            {
                throw new Exception("Assign faculty members to subjects before generating a schedule.");
            }
        }

        private static Schedule? CreateBestScheduleEntry(
            Subject subject,
            Section section,
            List<Classroom> classrooms,
            List<TimeSlot> timeSlots,
            List<FacultyMemberSubject> assignments,
            List<Schedule> allSchedules,
            Dictionary<int, int> facultyLoad,
            Dictionary<int, int> classroomLoad)
        {
            List<int> facultyIds = assignments
                .Where(a => a.SubjectID == subject.SubjectID)
                .Select(a => a.FacultyMemberID)
                .Distinct()
                .OrderBy(id => GetLoad(facultyLoad, id))
                .ToList();

            if (facultyIds.Count == 0)
            {
                return null;
            }

            List<Classroom> candidateClassrooms = classrooms
                .Where(c => c.Capacity >= section.StudentCount)
                .OrderBy(c => GetLoad(classroomLoad, c.ClassroomID))
                .ThenBy(c => c.Capacity)
                .ToList();

            if (candidateClassrooms.Count == 0)
            {
                return null;
            }

            Schedule? schedule = TryCreateScheduleEntry(
                subject,
                section,
                timeSlots,
                facultyIds,
                candidateClassrooms,
                allSchedules,
                facultyLoad,
                classroomLoad,
                avoidSameSubjectOnSameDay: true);

            return schedule ?? TryCreateScheduleEntry(
                subject,
                section,
                timeSlots,
                facultyIds,
                candidateClassrooms,
                allSchedules,
                facultyLoad,
                classroomLoad,
                avoidSameSubjectOnSameDay: false);
        }

        private static Schedule? TryCreateScheduleEntry(
            Subject subject,
            Section section,
            List<TimeSlot> timeSlots,
            List<int> facultyIds,
            List<Classroom> classrooms,
            List<Schedule> allSchedules,
            Dictionary<int, int> facultyLoad,
            Dictionary<int, int> classroomLoad,
            bool avoidSameSubjectOnSameDay)
        {
            foreach (string day in TeachingDays.OrderBy(day => GetDaySectionLoad(allSchedules, section.SectionID, day)))
            {
                foreach (TimeSlot timeSlot in timeSlots)
                {
                    if (avoidSameSubjectOnSameDay && HasSameSubjectForSectionOnDay(allSchedules, subject.SubjectID, section.SectionID, day))
                    {
                        continue;
                    }

                    foreach (int facultyId in facultyIds.OrderBy(id => GetLoad(facultyLoad, id)))
                    {
                        foreach (Classroom classroom in classrooms.OrderBy(c => GetLoad(classroomLoad, c.ClassroomID)).ThenBy(c => c.Capacity))
                        {
                            if (HasSchedulingConflict(allSchedules, facultyId, classroom.ClassroomID, section.SectionID, timeSlot.TimeSlotID, day))
                            {
                                continue;
                            }

                            return new Schedule
                            {
                                SubjectID = subject.SubjectID,
                                FacultyMemberID = facultyId,
                                ClassroomID = classroom.ClassroomID,
                                TimeSlotID = timeSlot.TimeSlotID,
                                DayOfWeek = day,
                                StudyYearID = section.StudyYearID,
                                BranchID = section.BranchID,
                                SectionID = section.SectionID
                            };
                        }
                    }
                }
            }

            return null;
        }

        private static bool SubjectMatchesSection(Subject subject, Section section)
        {
            return subject.StudyYearID == section.StudyYearID &&
                (!subject.BranchID.HasValue || subject.BranchID == section.BranchID);
        }

        private static int GetRequiredSessionCount(Subject subject)
        {
            double totalHours = subject.TheoreticalHours + subject.PracticalHours;
            double requiredSessions = totalHours > 0 ? totalHours : subject.CreditUnits;
            return Math.Max(1, (int)Math.Ceiling(requiredSessions));
        }

        private static bool HasSchedulingConflict(
            List<Schedule> schedules,
            int facultyMemberId,
            int classroomId,
            int sectionId,
            int timeSlotId,
            string dayOfWeek)
        {
            return schedules.Any(s =>
                s.DayOfWeek == dayOfWeek &&
                s.TimeSlotID == timeSlotId &&
                (s.FacultyMemberID == facultyMemberId ||
                 s.ClassroomID == classroomId ||
                 s.SectionID == sectionId));
        }

        private static bool HasSameSubjectForSectionOnDay(List<Schedule> schedules, int subjectId, int sectionId, string dayOfWeek)
        {
            return schedules.Any(s =>
                s.DayOfWeek == dayOfWeek &&
                s.SubjectID == subjectId &&
                s.SectionID == sectionId);
        }

        private static int GetDaySectionLoad(List<Schedule> schedules, int sectionId, string dayOfWeek)
        {
            return schedules.Count(s => s.SectionID == sectionId && s.DayOfWeek == dayOfWeek);
        }

        private static Dictionary<int, int> BuildLoadMap(IEnumerable<Schedule> schedules, Func<Schedule, int> keySelector)
        {
            return schedules
                .GroupBy(keySelector)
                .ToDictionary(group => group.Key, group => group.Count());
        }

        private static int GetLoad(Dictionary<int, int> loadMap, int id)
        {
            return loadMap.TryGetValue(id, out int value) ? value : 0;
        }

        private static void IncreaseLoad(Dictionary<int, int> loadMap, int id)
        {
            loadMap[id] = GetLoad(loadMap, id) + 1;
        }

        private static string NormalizeDay(string dayOfWeek)
        {
            dayOfWeek = dayOfWeek.Trim();

            if (string.IsNullOrWhiteSpace(dayOfWeek))
            {
                throw new Exception("Day of week is required.");
            }

            if (!ValidDays.Contains(dayOfWeek))
            {
                throw new Exception("Day of week is invalid.");
            }

            return ValidDays.First(d => d.Equals(dayOfWeek, StringComparison.OrdinalIgnoreCase));
        }

        private static void ValidateScheduleInput(
            AppDbContext context,
            int subjectId,
            int facultyMemberId,
            int classroomId,
            int timeSlotId,
            string dayOfWeek,
            int? studyYearId,
            int? branchId,
            int? sectionId)
        {
            if (subjectId <= 0)
            {
                throw new Exception("Please select a subject.");
            }

            if (facultyMemberId <= 0)
            {
                throw new Exception("Please select a faculty member.");
            }

            if (classroomId <= 0)
            {
                throw new Exception("Please select a classroom.");
            }

            if (timeSlotId <= 0)
            {
                throw new Exception("Please select a time slot.");
            }

            if (!studyYearId.HasValue || studyYearId.Value <= 0)
            {
                throw new Exception("Please select a study year.");
            }

            if (!sectionId.HasValue || sectionId.Value <= 0)
            {
                throw new Exception("Please select a section.");
            }

            Subject? subject = context.Subjects
                .FirstOrDefault(s => s.SubjectID == subjectId);

            if (subject == null)
            {
                throw new Exception("Selected subject does not exist.");
            }

            FacultyMember? facultyMember = context.FacultyMembers
                .FirstOrDefault(f => f.FacultyMemberID == facultyMemberId);

            if (facultyMember == null)
            {
                throw new Exception("Selected faculty member does not exist.");
            }

            Classroom? classroom = context.Classrooms
                .FirstOrDefault(c => c.ClassroomID == classroomId);

            if (classroom == null)
            {
                throw new Exception("Selected classroom does not exist.");
            }

            TimeSlot? timeSlot = context.TimeSlots
                .FirstOrDefault(t => t.TimeSlotID == timeSlotId);

            if (timeSlot == null)
            {
                throw new Exception("Selected time slot does not exist.");
            }

            if (timeSlot.IsBreak)
            {
                throw new Exception("Cannot schedule a class during a break time slot.");
            }

            bool canTeachSubject = context.FacultyMemberSubjects
                .Any(a => a.FacultyMemberID == facultyMemberId && a.SubjectID == subjectId);

            if (!canTeachSubject)
            {
                throw new Exception("Selected faculty member is not assigned to teach this subject.");
            }

            if (subject.StudyYearID != studyYearId.Value)
            {
                throw new Exception("Selected subject does not belong to the selected study year.");
            }

            if (subject.BranchID.HasValue && subject.BranchID != branchId)
            {
                throw new Exception("Selected subject belongs to a different branch.");
            }

            if (!context.StudyYears.Any(y => y.StudyYearID == studyYearId.Value))
            {
                throw new Exception("Selected study year does not exist.");
            }

            if (branchId.HasValue && !context.Branches.Any(b => b.BranchID == branchId.Value))
            {
                throw new Exception("Selected branch does not exist.");
            }

            Section? section = context.Sections
                .FirstOrDefault(s => s.SectionID == sectionId.Value);

            if (section == null)
            {
                throw new Exception("Selected section does not exist.");
            }

            if (section.StudyYearID != studyYearId.Value)
            {
                throw new Exception("Selected section does not belong to the selected study year.");
            }

            if (branchId.HasValue && section.BranchID != branchId.Value)
            {
                throw new Exception("Selected section does not belong to the selected branch.");
            }

            if (!branchId.HasValue && section.BranchID.HasValue)
            {
                throw new Exception("Please select the branch that belongs to the selected section.");
            }

            if (section.StudentCount > classroom.Capacity)
            {
                throw new Exception("Classroom capacity is less than the selected section student count.");
            }

            _ = dayOfWeek;
        }

        private static void EnsureNoScheduleConflicts(
            AppDbContext context,
            int scheduleId,
            int facultyMemberId,
            int classroomId,
            int timeSlotId,
            string dayOfWeek,
            int? sectionId)
        {
            bool facultyConflict = context.Schedules
                .Any(s =>
                    s.ScheduleID != scheduleId &&
                    s.DayOfWeek == dayOfWeek &&
                    s.TimeSlotID == timeSlotId &&
                    s.FacultyMemberID == facultyMemberId);

            if (facultyConflict)
            {
                throw new Exception("Faculty member already has a class at this day and time.");
            }

            bool classroomConflict = context.Schedules
                .Any(s =>
                    s.ScheduleID != scheduleId &&
                    s.DayOfWeek == dayOfWeek &&
                    s.TimeSlotID == timeSlotId &&
                    s.ClassroomID == classroomId);

            if (classroomConflict)
            {
                throw new Exception("Classroom is already used at this day and time.");
            }

            if (sectionId.HasValue)
            {
                bool sectionConflict = context.Schedules
                    .Any(s =>
                        s.ScheduleID != scheduleId &&
                        s.DayOfWeek == dayOfWeek &&
                        s.TimeSlotID == timeSlotId &&
                        s.SectionID == sectionId.Value);

                if (sectionConflict)
                {
                    throw new Exception("Section already has a class at this day and time.");
                }
            }
        }

        private static int GetDayOrder(string dayOfWeek)
        {
            int index = Array.FindIndex(TeachingDays, day => day.Equals(dayOfWeek, StringComparison.OrdinalIgnoreCase));

            if (index >= 0)
            {
                return index;
            }

            return dayOfWeek switch
            {
                "Friday" => 5,
                "Saturday" => 6,
                _ => 7
            };
        }
    }

    public sealed class ScheduleGenerationResult
    {
        public ScheduleGenerationResult(int createdCount, int skippedCount, IReadOnlyList<string> warnings, int? semesterNumber)
        {
            CreatedCount = createdCount;
            SkippedCount = skippedCount;
            Warnings = warnings;
            SemesterNumber = semesterNumber;
        }

        public int CreatedCount { get; }

        public int SkippedCount { get; }

        public int? SemesterNumber { get; }

        public IReadOnlyList<string> Warnings { get; }
    }
}
