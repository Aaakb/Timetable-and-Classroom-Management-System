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
                .OrderBy(s => s.DayOfWeek)
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

            if (studyYearId.HasValue && !context.StudyYears.Any(y => y.StudyYearID == studyYearId.Value))
            {
                throw new Exception("Selected study year does not exist.");
            }

            if (branchId.HasValue && !context.Branches.Any(b => b.BranchID == branchId.Value))
            {
                throw new Exception("Selected branch does not exist.");
            }

            Section? section = null;

            if (sectionId.HasValue)
            {
                section = context.Sections
                    .FirstOrDefault(s => s.SectionID == sectionId.Value);

                if (section == null)
                {
                    throw new Exception("Selected section does not exist.");
                }

                if (studyYearId.HasValue && section.StudyYearID != studyYearId.Value)
                {
                    throw new Exception("Selected section does not belong to the selected study year.");
                }

                if (branchId.HasValue && section.BranchID != branchId.Value)
                {
                    throw new Exception("Selected section does not belong to the selected branch.");
                }

                if (section.StudentCount > classroom.Capacity)
                {
                    throw new Exception("Classroom capacity is less than the selected section student count.");
                }
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
    }
}
