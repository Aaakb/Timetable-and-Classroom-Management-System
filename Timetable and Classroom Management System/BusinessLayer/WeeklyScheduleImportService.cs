using Timetable_and_Classroom_Management_System.DataAccessLayer;
using Timetable_and_Classroom_Management_System.Models;

namespace Timetable_and_Classroom_Management_System.BusinessLayer
{
    public class WeeklyScheduleImportService
    {
        public WeeklyScheduleImportResult ImportSecondSemester2026Schedule()
        {
            using AppDbContext context = new AppDbContext();

            int addedSchedules = 0;
            int existingSchedules = 0;
            int skippedSchedules = 0;
            List<string> warnings = new List<string>();

            foreach (WeeklyScheduleSeed seed in ScheduleSeeds)
            {
                string studyYearName = ReferenceNameNormalizer.NormalizeStudyYearName(seed.StudyYear);
                string? branchName = seed.BranchName == null
                    ? null
                    : ReferenceNameNormalizer.NormalizeBranchName(seed.BranchName);
                StudyYear studyYear = EnsureStudyYear(context, studyYearName);
                Branch? branch = branchName == null ? null : EnsureBranch(context, branchName);
                Section section = EnsureSection(context, seed.SectionName, studyYear.StudyYearID, branch?.BranchID);
                Subject subject = EnsureSubject(context, seed.SubjectName, studyYear.StudyYearID, branch?.BranchID, seed.IsPractical);
                FacultyMember facultyMember = EnsureFacultyMember(context, seed.FacultyName);
                Classroom classroom = EnsureClassroom(context, seed.ClassroomName);
                TimeSlot timeSlot = EnsureTimeSlot(context, seed.StartTime, seed.EndTime);
                EnsureAssignment(context, facultyMember.FacultyMemberID, subject.SubjectID);

                Schedule? existing = context.Schedules.FirstOrDefault(s =>
                    s.SubjectID == subject.SubjectID &&
                    s.FacultyMemberID == facultyMember.FacultyMemberID &&
                    s.ClassroomID == classroom.ClassroomID &&
                    s.TimeSlotID == timeSlot.TimeSlotID &&
                    s.DayOfWeek == seed.DayOfWeek &&
                    s.StudyYearID == studyYear.StudyYearID &&
                    s.BranchID == (branch == null ? null : branch.BranchID) &&
                    s.SectionID == section.SectionID);

                if (existing != null)
                {
                    existingSchedules++;
                    continue;
                }

                bool hasConflict = context.Schedules.Any(s =>
                    s.DayOfWeek == seed.DayOfWeek &&
                    s.TimeSlotID == timeSlot.TimeSlotID &&
                    (s.FacultyMemberID == facultyMember.FacultyMemberID ||
                     s.ClassroomID == classroom.ClassroomID ||
                     s.SectionID == section.SectionID));

                if (hasConflict)
                {
                    skippedSchedules++;
                    warnings.Add($"{studyYearName} {seed.SectionName}: {seed.SubjectName} on {seed.DayOfWeek} {seed.StartTime:hh\\:mm} was skipped because it conflicts with an existing entry.");
                    continue;
                }

                context.Schedules.Add(new Schedule
                {
                    SubjectID = subject.SubjectID,
                    FacultyMemberID = facultyMember.FacultyMemberID,
                    ClassroomID = classroom.ClassroomID,
                    TimeSlotID = timeSlot.TimeSlotID,
                    DayOfWeek = seed.DayOfWeek,
                    StudyYearID = studyYear.StudyYearID,
                    BranchID = branch?.BranchID,
                    SectionID = section.SectionID
                });

                addedSchedules++;
            }

            context.SaveChanges();

            return new WeeklyScheduleImportResult(addedSchedules, existingSchedules, skippedSchedules, warnings);
        }

        private static StudyYear EnsureStudyYear(AppDbContext context, string yearName)
        {
            yearName = ReferenceNameNormalizer.NormalizeStudyYearName(yearName);

            StudyYear? studyYear = context.StudyYears.FirstOrDefault(y => y.YearName == yearName);

            if (studyYear != null)
            {
                return studyYear;
            }

            studyYear = new StudyYear { YearName = yearName };
            context.StudyYears.Add(studyYear);
            context.SaveChanges();
            return studyYear;
        }

        private static Branch EnsureBranch(AppDbContext context, string branchName)
        {
            branchName = ReferenceNameNormalizer.NormalizeBranchName(branchName);

            Branch? branch = context.Branches.FirstOrDefault(b => b.BranchName == branchName);

            if (branch != null)
            {
                return branch;
            }

            branch = new Branch { BranchName = branchName };
            context.Branches.Add(branch);
            context.SaveChanges();
            return branch;
        }

        private static Section EnsureSection(AppDbContext context, string sectionName, int studyYearId, int? branchId)
        {
            Section? section = context.Sections.FirstOrDefault(s =>
                s.SectionName == sectionName &&
                s.StudyYearID == studyYearId &&
                s.BranchID == branchId);

            if (section != null)
            {
                return section;
            }

            section = new Section
            {
                SectionName = sectionName,
                StudyYearID = studyYearId,
                BranchID = branchId,
                StudentCount = sectionName.Contains('1') || sectionName.Contains('2') ? 20 : 40
            };

            context.Sections.Add(section);
            context.SaveChanges();
            return section;
        }

        private static Subject EnsureSubject(AppDbContext context, string subjectName, int studyYearId, int? branchId, bool isPractical)
        {
            Subject? subject = context.Subjects.FirstOrDefault(s =>
                s.SubjectName == subjectName &&
                s.StudyYearID == studyYearId &&
                s.SemesterNumber == 2 &&
                s.BranchID == branchId);

            if (subject != null)
            {
                return subject;
            }

            subject = new Subject
            {
                SubjectName = subjectName,
                StudyYearID = studyYearId,
                SemesterNumber = 2,
                TheoreticalHours = isPractical ? 0 : 1,
                PracticalHours = isPractical ? 1 : 0,
                CreditUnits = 1,
                RequirementType = "Imported Schedule",
                BranchID = branchId
            };

            context.Subjects.Add(subject);
            context.SaveChanges();
            return subject;
        }

        private static FacultyMember EnsureFacultyMember(AppDbContext context, string fullName)
        {
            FacultyMember? facultyMember = context.FacultyMembers.FirstOrDefault(f => f.FullName == fullName);

            if (facultyMember != null)
            {
                return facultyMember;
            }

            facultyMember = new FacultyMember { FullName = fullName };
            context.FacultyMembers.Add(facultyMember);
            context.SaveChanges();
            return facultyMember;
        }

        private static Classroom EnsureClassroom(AppDbContext context, string classroomNumber)
        {
            Classroom? classroom = context.Classrooms.FirstOrDefault(c => c.ClassroomNumber == classroomNumber);

            if (classroom != null)
            {
                return classroom;
            }

            bool isLab = classroomNumber.StartsWith("مختبر", StringComparison.OrdinalIgnoreCase);

            classroom = new Classroom
            {
                ClassroomNumber = classroomNumber,
                Capacity = isLab ? 30 : 60,
                RoomType = isLab ? "Lab" : "Lecture"
            };

            context.Classrooms.Add(classroom);
            context.SaveChanges();
            return classroom;
        }

        private static TimeSlot EnsureTimeSlot(AppDbContext context, TimeSpan startTime, TimeSpan endTime)
        {
            TimeSlot? timeSlot = context.TimeSlots.FirstOrDefault(t =>
                t.StartTime == startTime &&
                t.EndTime == endTime);

            if (timeSlot != null)
            {
                return timeSlot;
            }

            timeSlot = new TimeSlot
            {
                StartTime = startTime,
                EndTime = endTime,
                IsBreak = false
            };

            context.TimeSlots.Add(timeSlot);
            context.SaveChanges();
            return timeSlot;
        }

        private static void EnsureAssignment(AppDbContext context, int facultyMemberId, int subjectId)
        {
            bool exists = context.FacultyMemberSubjects.Any(a =>
                a.FacultyMemberID == facultyMemberId &&
                a.SubjectID == subjectId);

            if (exists)
            {
                return;
            }

            context.FacultyMemberSubjects.Add(new FacultyMemberSubject
            {
                FacultyMemberID = facultyMemberId,
                SubjectID = subjectId
            });

            context.SaveChanges();
        }

        private static readonly string[] RawScheduleEntries =
        {
            "المرحلة الأولى||A1|Sunday|08:30|09:50|Computer Skills (II)|م.م داليا شهاب|مختبر 1",
            "المرحلة الأولى||A2|Sunday|08:30|09:50|Programming Fundamentals (II)|م. حنان عبد الولي|مختبر 3",
            "المرحلة الأولى||A2|Sunday|10:00|11:20|Programming Fundamentals (II)|م. حنان عبد الولي|مختبر 1",
            "المرحلة الأولى||A2|Sunday|10:00|11:20|Computer Skills (II)|م.م داليا شهاب|مختبر 2",
            "المرحلة الأولى||A1|Sunday|11:30|12:50|Logic Design|د. رفاء إسماعيل|مختبر 1",
            "المرحلة الأولى||A2|Monday|08:30|09:50|Logic Design|د. رفاء إسماعيل|مختبر 1",
            "المرحلة الأولى||A|Monday|10:00|11:20|Computer Skills (II)|م. زياد فاروق|قاعة 402",
            "المرحلة الأولى||A|Tuesday|10:00|11:20|Information Technology|د. فرح قيس|قاعة 401",
            "المرحلة الأولى||A|Tuesday|11:30|12:50|Logic Design|د. ورود عبد الكريم|قاعة 301",
            "المرحلة الأولى||A|Wednesday|08:30|09:50|English Language|م.م سعد بادي|قاعة 301",
            "المرحلة الأولى||A|Wednesday|10:00|11:20|Mathematics|م. منى جعفر|قاعة 401",
            "المرحلة الأولى||A|Wednesday|11:30|12:50|Programming Fundamentals (II)|د. جلال حميد|قاعة 301",

            "المرحلة الأولى||B|Sunday|08:30|09:50|English Language|م.م سعد بادي|قاعة 402",
            "المرحلة الأولى||B2|Sunday|10:00|11:20|Logic Design|د. رفاء إسماعيل|مختبر 7",
            "المرحلة الأولى||B|Sunday|11:30|12:50|Computer Skills (II)|م. زياد فاروق|قاعة 402",
            "المرحلة الأولى||B|Monday|10:00|11:20|Logic Design|د. ورود عبد الكريم|قاعة 302",
            "المرحلة الأولى||B|Monday|11:30|12:50|Mathematics|م. منى جعفر|قاعة 401",
            "المرحلة الأولى||B|Tuesday|10:00|11:20|Programming Fundamentals (II)|د. جلال حميد|مختبر 2",
            "المرحلة الأولى||B1|Tuesday|11:30|12:50|Logic Design|د. رفاء إسماعيل|مختبر 7",
            "المرحلة الأولى||B1|Wednesday|08:30|09:50|Programming Fundamentals (II)|م.د حنان عبد الولي|مختبر 5",
            "المرحلة الأولى||B2|Wednesday|08:30|09:50|Computer Skills (II)|م.م داليا شهاب|مختبر 2",
            "المرحلة الأولى||B|Wednesday|10:00|11:20|Information Technology|د. فرح قيس|قاعة 301",
            "المرحلة الأولى||B1|Wednesday|11:30|12:50|Computer Skills (II)|م.م داليا شهاب|مختبر 2",
            "المرحلة الأولى||B2|Wednesday|11:30|12:50|Programming Fundamentals (II)|م.د حنان عبد الولي|مختبر 5",

            "المرحلة الثانية||A1|Sunday|10:00|11:20|Algorithms Design and Analysis|م. قبس عبد الزهرة|مختبر 3",
            "المرحلة الثانية||A2|Sunday|10:00|11:20|Python Programming|م.م حوراء عطوف|مختبر 5",
            "المرحلة الثانية||A1|Sunday|11:30|12:50|Python Programming|م.م حوراء عطوف|مختبر 5",
            "المرحلة الثانية||A2|Sunday|11:30|12:50|Algorithms Design and Analysis|م. قبس عبد الزهرة|مختبر 2",
            "المرحلة الثانية||A1|Monday|08:30|09:50|Compiler|أ.م. منى عبد الحسين|مختبر 2",
            "المرحلة الثانية||A2|Monday|08:30|09:50|Database Fundamentals|م. روسم عبد العظيم|مختبر 5",
            "المرحلة الثانية||A1|Monday|10:00|11:20|Database Fundamentals|م. روسم عبد العظيم|مختبر 5",
            "المرحلة الثانية||A2|Monday|10:00|11:20|Compiler|أ.م. منى عبد الحسين|مختبر 2",
            "المرحلة الثانية||A|Monday|11:30|12:50|The Crime of Baath Regime in Iraq|Imported Instructor|قاعة 301",
            "المرحلة الثانية||A|Tuesday|08:30|09:50|Algorithms Design and Analysis|د. ورود عبد الكريم|قاعة 301",
            "المرحلة الثانية||A|Tuesday|10:00|11:20|Python Programming|أ.م ياسمين مكي|قاعة 402",
            "المرحلة الثانية||A|Tuesday|11:30|12:50|Database Fundamentals|د. جلال حميد|قاعة 402",
            "المرحلة الثانية||A|Thursday|08:30|09:50|Compiler|د. باسم جميل|قاعة 301",
            "المرحلة الثانية||A|Thursday|10:00|11:20|Arabic Language|م.م بتول زكي|قاعة 301",
            "المرحلة الثانية||A|Thursday|11:30|12:50|Statistics and Probability|د. حازم مجمان|قاعة 302",

            "المرحلة الثانية||B|Sunday|08:30|09:50|Algorithms Design and Analysis|د. ورود عبد الكريم|قاعة 302",
            "المرحلة الثانية||B|Sunday|10:00|11:20|Python Programming|أ.م ياسمين مكي|قاعة 302",
            "المرحلة الثانية||B|Sunday|11:30|12:50|Arabic Language|م.م بتول زكي|قاعة 302",
            "المرحلة الثانية||B|Monday|08:30|09:50|Compiler|د. باسم جميل|قاعة 401",
            "المرحلة الثانية||B|Monday|10:00|11:20|The Crime of Baath Regime in Iraq|Imported Instructor|قاعة 301",
            "المرحلة الثانية||B|Monday|11:30|12:50|Database Fundamentals|د. جلال حميد|قاعة 402",
            "المرحلة الثانية||B1|Tuesday|08:30|09:50|Algorithms Design and Analysis|م. قبس عبد الزهرة|مختبر 2",
            "المرحلة الثانية||B2|Tuesday|08:30|09:50|Python Programming|د. الاء سنان|مختبر 5",
            "المرحلة الثانية||B|Tuesday|10:00|11:20|Statistics and Probability|د. حازم مجمان|قاعة 301",
            "المرحلة الثانية||B1|Tuesday|11:30|12:50|Python Programming|د. الاء سنان|مختبر 5",
            "المرحلة الثانية||B2|Tuesday|11:30|12:50|Algorithms Design and Analysis|م. قبس عبد الزهرة|مختبر 2",
            "المرحلة الثانية||B1|Thursday|08:30|09:50|Compiler|أ.م. منى عبد الحسين|مختبر 2",
            "المرحلة الثانية||B2|Thursday|08:30|09:50|Database Fundamentals|م. روسم عبد العظيم|مختبر 5",
            "المرحلة الثانية||B1|Thursday|10:00|11:20|Database Fundamentals|م. روسم عبد العظيم|مختبر 5",
            "المرحلة الثانية||B2|Thursday|10:00|11:20|Compiler|أ.م. منى عبد الحسين|مختبر 2",

            "المرحلة الثالثة|علوم الحاسوب|CS A2|Sunday|08:30|09:50|Computer Networks|م. مهند علي|مختبر 5",
            "المرحلة الثالثة|علوم الحاسوب|CS|Sunday|10:00|11:20|Information Security|د. ازهار حسن|قاعة 301",
            "المرحلة الثالثة|علوم الحاسوب|CS|Sunday|11:30|12:50|Computer Networks|د. عباس عبد العزيز|مختبر 3",
            "المرحلة الثالثة|علوم الحاسوب|CS|Monday|08:30|09:50|Machine learning|د. مها ادهم|مختبر 3",
            "المرحلة الثالثة|علوم الحاسوب|CS|Monday|10:00|11:20|Parallel Computing|أ.م انوار حسن|قاعة 402",
            "المرحلة الثالثة|علوم الحاسوب|CS|Monday|11:30|12:50|Open-Source Systems|د. محمد عزيز|قاعة 401",
            "المرحلة الثالثة|علوم الحاسوب|CS A1|Tuesday|08:30|09:50|Open-Source Systems|أ.م زياد فاروق|مختبر 3",
            "المرحلة الثالثة|علوم الحاسوب|CS A1|Tuesday|10:00|11:20|Machine learning|م.م انبثاق احمد|مختبر 3",
            "المرحلة الثالثة|علوم الحاسوب|CS A1|Wednesday|08:30|09:50|Information Security|د. ازهار حسن|مختبر 1",
            "المرحلة الثالثة|علوم الحاسوب|CS A2|Wednesday|08:30|09:50|Multimedia|د. جميلة حربي|مختبر 3",
            "المرحلة الثالثة|علوم الحاسوب|CS A1|Wednesday|10:00|11:20|Multimedia|د. جميلة حربي|مختبر 3",
            "المرحلة الثالثة|علوم الحاسوب|CS A2|Wednesday|10:00|11:20|Information Security|د. ازهار حسن|مختبر 1",
            "المرحلة الثالثة|علوم الحاسوب|CS|Wednesday|11:30|12:50|Multimedia|د. جميلة حربي|قاعة 402",

            "المرحلة الثالثة|الأمن السيبراني|Cy|Sunday|08:30|09:50|Multimedia security|د. اسماء صادق|قاعة 301",
            "المرحلة الثالثة|الأمن السيبراني|Cy|Sunday|10:00|11:20|Website security|د. بشار مكي|قاعة 401",
            "المرحلة الثالثة|الأمن السيبراني|Cy|Sunday|11:30|12:50|Artificial Intelligence|د. زيد عثمان|قاعة 401",
            "المرحلة الثالثة|الأمن السيبراني|Cy|Tuesday|08:30|09:50|Computer Networks|د. باسم جميل|قاعة 402",
            "المرحلة الثالثة|الأمن السيبراني|Cy A1|Tuesday|10:00|11:20|Multimedia security|م. مثنى صالح|مختبر 5",
            "المرحلة الثالثة|الأمن السيبراني|Cy A2|Tuesday|10:00|11:20|Artificial Intelligence|م. فراس علي|مختبر 1",
            "المرحلة الثالثة|الأمن السيبراني|Cy A1|Tuesday|11:30|12:50|Artificial Intelligence|م. فراس علي|مختبر 1",
            "المرحلة الثالثة|الأمن السيبراني|Cy A2|Tuesday|11:30|12:50|Multimedia security|م. مثنى صالح|مختبر 3",
            "المرحلة الثالثة|الأمن السيبراني|Cy|Wednesday|08:30|09:50|Kali Linux Operating System|د. ياسمين حمزة|قاعة 401",
            "المرحلة الثالثة|الأمن السيبراني|Cy A1|Wednesday|10:00|11:20|Computer Networks|م. مهند علي|مختبر 2",
            "المرحلة الثالثة|الأمن السيبراني|Cy A2|Wednesday|10:00|11:20|Kali Linux Operating System|د. نور ثامر|مختبر 5",
            "المرحلة الثالثة|الأمن السيبراني|Cy A1|Wednesday|11:30|12:50|Kali Linux Operating System|د. نور ثامر|مختبر 1",
            "المرحلة الثالثة|الأمن السيبراني|Cy A2|Wednesday|11:30|12:50|Computer Networks|م. مهند علي|مختبر 3",
            "المرحلة الثالثة|الأمن السيبراني|Cy|Thursday|08:30|09:50|Cryptography|د. ايمان هاتو|قاعة 401",
            "المرحلة الثالثة|الأمن السيبراني|Cy A1|Thursday|10:00|11:20|Website security|م.م ايمن ماجد|مختبر 3",
            "المرحلة الثالثة|الأمن السيبراني|Cy A2|Thursday|10:00|11:20|Cryptography|د. ايمان هاتو|مختبر 6",
            "المرحلة الثالثة|الأمن السيبراني|Cy A1|Thursday|11:30|12:50|Cryptography|د. ايمان هاتو|مختبر 6",
            "المرحلة الثالثة|الأمن السيبراني|Cy A2|Thursday|11:30|12:50|Website security|م.م ايمن ماجد|مختبر 3",

            "المرحلة الثالثة|تكنولوجيا المعلومات|IT|Sunday|08:30|09:50|Green IT and Sustainable Computing|د. هدى عبد العالي|مختبر 2",
            "المرحلة الثالثة|تكنولوجيا المعلومات|IT A1|Sunday|10:00|11:20|User Interface and User Experience (UI/UX) Design|م.د الاء سنان|مختبر 6",
            "المرحلة الثالثة|تكنولوجيا المعلومات|IT A2|Sunday|10:00|11:20|Artificial Intelligence|م. فراس علي|مختبر 4",
            "المرحلة الثالثة|تكنولوجيا المعلومات|IT A1|Sunday|11:30|12:50|Artificial Intelligence|م. فراس علي|مختبر 4",
            "المرحلة الثالثة|تكنولوجيا المعلومات|IT A2|Sunday|11:30|12:50|User Interface and User Experience (UI/UX) Design|م.د الاء سنان|مختبر 6",
            "المرحلة الثالثة|تكنولوجيا المعلومات|IT|Monday|08:30|09:50|User Interface and User Experience (UI/UX) Design|د. الاء سنان|قاعة 402",
            "المرحلة الثالثة|تكنولوجيا المعلومات|IT A1|Monday|10:00|11:20|Information Systems Security|م.م شذى جاسم|مختبر 4",
            "المرحلة الثالثة|تكنولوجيا المعلومات|IT A2|Monday|10:00|11:20|Green IT and Sustainable Computing|أ.م انوار حسن|مختبر 3",
            "المرحلة الثالثة|تكنولوجيا المعلومات|IT A1|Monday|11:30|12:50|Green IT and Sustainable Computing|أ.م انوار حسن|مختبر 3",
            "المرحلة الثالثة|تكنولوجيا المعلومات|IT A2|Monday|11:30|12:50|Information Systems Security|م.م شذى جاسم|مختبر 1",
            "المرحلة الثالثة|تكنولوجيا المعلومات|IT|Wednesday|08:30|09:50|Artificial Intelligence|د. مهند تحرير|قاعة 402",
            "المرحلة الثالثة|تكنولوجيا المعلومات|IT|Wednesday|10:00|11:20|Operating Systems|م. رياض حازم|قاعة 402",
            "المرحلة الثالثة|تكنولوجيا المعلومات|IT|Wednesday|11:30|12:50|DevOps and Software Testing|د. ورود حسن|قاعة 402",
            "المرحلة الثالثة|تكنولوجيا المعلومات|IT|Thursday|08:30|09:50|Information Systems Security|د. حنان عبد الولي|قاعة 402",
            "المرحلة الثالثة|تكنولوجيا المعلومات|IT A1|Thursday|10:00|11:20|Operating Systems|م. رياض حازم|مختبر 4",
            "المرحلة الثالثة|تكنولوجيا المعلومات|IT A2|Thursday|10:00|11:20|DevOps and Software Testing|د. ورود حسن|مختبر 1",
            "المرحلة الثالثة|تكنولوجيا المعلومات|IT A1|Thursday|11:30|12:50|DevOps and Software Testing|د. ورود حسن|مختبر 1",
            "المرحلة الثالثة|تكنولوجيا المعلومات|IT A2|Thursday|11:30|12:50|Operating Systems|م. رياض حازم|مختبر 4",

            "المرحلة الرابعة|علوم الحاسوب|CS4|Tuesday|08:30|09:50|Cloud Computing|د. ايثار عبد الوهاب|قاعة 401",
            "المرحلة الرابعة|علوم الحاسوب|CS4|Tuesday|10:00|11:20|Intelligent Agent|م. نادية محمود|مختبر 6",
            "المرحلة الرابعة|علوم الحاسوب|CS4|Wednesday|08:30|09:50|Cloud Computing|د. ايثار عبد الوهاب|مختبر 6",
            "المرحلة الرابعة|علوم الحاسوب|CS4|Wednesday|10:00|11:20|Intelligent Agent|د. مها ادهم|مختبر 4",
            "المرحلة الرابعة|علوم الحاسوب|CS4|Wednesday|11:30|12:50|Multimedia|م.م مثنى صالح|مختبر 6",
            "المرحلة الرابعة|علوم الحاسوب|CS4|Thursday|08:30|09:50|English Language|م.م سعد بادي|مختبر 3",
            "المرحلة الرابعة|علوم الحاسوب|CS4|Thursday|10:00|11:20|Multimedia|د. مصعب رياض|قاعة 401",

            "المرحلة الرابعة|الأمن السيبراني|Cy4|Sunday|08:30|09:50|Digital Forensics|د. بشار مكي|قاعة 401",
            "المرحلة الرابعة|الأمن السيبراني|Cy4|Sunday|10:00|11:20|Data hiding and watermarking|د. سعد نجم|قاعة 402",
            "المرحلة الرابعة|الأمن السيبراني|Cy4|Monday|08:30|09:50|Operating Systems|د. عباس اكرم|مختبر 4",
            "المرحلة الرابعة|الأمن السيبراني|Cy4|Monday|10:00|11:20|Operating Systems|م. رياض حازم|مختبر 6",
            "المرحلة الرابعة|الأمن السيبراني|Cy4|Wednesday|08:30|09:50|Data hiding and watermarking|د. سعد نجم|مختبر 4",
            "المرحلة الرابعة|الأمن السيبراني|Cy4|Wednesday|10:00|11:20|Database Security|د. ورود حسن|قاعة 401",
            "المرحلة الرابعة|الأمن السيبراني|Cy4|Wednesday|11:30|12:50|Database Security|م.م مهند مؤيد|مختبر 7",
            "المرحلة الرابعة|الأمن السيبراني|Cy4|Thursday|08:30|09:50|Digital Forensics|د. ايمن ماجد|مختبر 6",
            "المرحلة الرابعة|الأمن السيبراني|Cy4|Thursday|10:00|11:20|English Language|م.م سعد بادي|قاعة 402",

            "المرحلة الرابعة|تكنولوجيا المعلومات|IT4|Monday|08:30|09:50|Digital Forensics|م. حيدر حميد|مختبر 6",
            "المرحلة الرابعة|تكنولوجيا المعلومات|IT4|Monday|10:00|11:20|Cloud Computing|د. رفاء إسماعيل|مختبر 7",
            "المرحلة الرابعة|تكنولوجيا المعلومات|IT4|Monday|11:30|12:50|Digital Forensics|م. حيدر حميد|مختبر 4",
            "المرحلة الرابعة|تكنولوجيا المعلومات|IT4|Tuesday|08:30|09:50|Cloud Computing|د. رفاء إسماعيل|مختبر 4",
            "المرحلة الرابعة|تكنولوجيا المعلومات|IT4|Tuesday|10:00|11:20|Multimedia|د. ابراهيم نضير|مختبر 4",
            "المرحلة الرابعة|تكنولوجيا المعلومات|IT4|Wednesday|08:30|09:50|Information Systems Security|م.م شذى جاسم|مختبر 7",
            "المرحلة الرابعة|تكنولوجيا المعلومات|IT4|Wednesday|10:00|11:20|Multimedia|م. مثنى صالح|مختبر 6",
            "المرحلة الرابعة|تكنولوجيا المعلومات|IT4|Thursday|10:00|11:20|English Language|م.م سعد بادي|قاعة 402",
            "المرحلة الرابعة|تكنولوجيا المعلومات|IT4|Thursday|11:30|12:50|Information Systems Security|د. مهند تحرير|مختبر 7"
        };

        private static readonly IReadOnlyList<WeeklyScheduleSeed> ScheduleSeeds = RawScheduleEntries
            .Select(WeeklyScheduleSeed.Parse)
            .ToList();

        private sealed class WeeklyScheduleSeed
        {
            private WeeklyScheduleSeed(
                string studyYear,
                string? branchName,
                string sectionName,
                string dayOfWeek,
                TimeSpan startTime,
                TimeSpan endTime,
                string subjectName,
                string facultyName,
                string classroomName)
            {
                StudyYear = studyYear;
                BranchName = branchName;
                SectionName = sectionName;
                DayOfWeek = dayOfWeek;
                StartTime = startTime;
                EndTime = endTime;
                SubjectName = subjectName;
                FacultyName = facultyName;
                ClassroomName = classroomName;
                IsPractical = classroomName.StartsWith("مختبر", StringComparison.OrdinalIgnoreCase) ||
                    sectionName.EndsWith("1", StringComparison.OrdinalIgnoreCase) ||
                    sectionName.EndsWith("2", StringComparison.OrdinalIgnoreCase);
            }

            public string StudyYear { get; }

            public string? BranchName { get; }

            public string SectionName { get; }

            public string DayOfWeek { get; }

            public TimeSpan StartTime { get; }

            public TimeSpan EndTime { get; }

            public string SubjectName { get; }

            public string FacultyName { get; }

            public string ClassroomName { get; }

            public bool IsPractical { get; }

            public static WeeklyScheduleSeed Parse(string value)
            {
                string[] parts = value.Split('|');

                if (parts.Length != 9)
                {
                    throw new InvalidOperationException("Invalid weekly schedule seed: " + value);
                }

                return new WeeklyScheduleSeed(
                    parts[0],
                    string.IsNullOrWhiteSpace(parts[1]) ? null : parts[1],
                    parts[2],
                    parts[3],
                    TimeSpan.Parse(parts[4]),
                    TimeSpan.Parse(parts[5]),
                    parts[6],
                    parts[7],
                    parts[8]);
            }
        }
    }

    public sealed class WeeklyScheduleImportResult
    {
        public WeeklyScheduleImportResult(
            int addedScheduleCount,
            int existingScheduleCount,
            int skippedScheduleCount,
            IReadOnlyList<string> warnings)
        {
            AddedScheduleCount = addedScheduleCount;
            ExistingScheduleCount = existingScheduleCount;
            SkippedScheduleCount = skippedScheduleCount;
            Warnings = warnings;
        }

        public int AddedScheduleCount { get; }

        public int ExistingScheduleCount { get; }

        public int SkippedScheduleCount { get; }

        public IReadOnlyList<string> Warnings { get; }
    }
}
