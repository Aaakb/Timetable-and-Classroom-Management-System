using Microsoft.EntityFrameworkCore;
using Timetable_and_Classroom_Management_System.Models;

namespace Timetable_and_Classroom_Management_System.DataAccessLayer
{
    public static class DatabaseSchemaService
    {
        public static void EnsureCompatibleSchema()
        {
            using AppDbContext context = new AppDbContext();

            context.Database.ExecuteSqlRaw(@"
IF OBJECT_ID(N'dbo.Schedules', N'U') IS NOT NULL
BEGIN
    IF EXISTS (
        SELECT 1
        FROM sys.key_constraints
        WHERE parent_object_id = OBJECT_ID(N'dbo.Schedules')
            AND name = N'UQ_Year_Branch_Time'
    )
    BEGIN
        ALTER TABLE dbo.Schedules DROP CONSTRAINT UQ_Year_Branch_Time;
    END;
    ELSE IF EXISTS (
        SELECT 1
        FROM sys.indexes
        WHERE object_id = OBJECT_ID(N'dbo.Schedules')
            AND name = N'UQ_Year_Branch_Time'
    )
    BEGIN
        DROP INDEX UQ_Year_Branch_Time ON dbo.Schedules;
    END;

    IF NOT EXISTS (
        SELECT 1
        FROM sys.indexes
        WHERE object_id = OBJECT_ID(N'dbo.Schedules')
            AND name = N'UQ_Section_Time'
    )
    BEGIN
        CREATE UNIQUE INDEX UQ_Section_Time
        ON dbo.Schedules(SectionID, DayOfWeek, TimeSlotID)
        WHERE SectionID IS NOT NULL;
    END;
END;
");
            EnsureEnglishReferenceData(context);
        }

        private static void EnsureEnglishReferenceData(AppDbContext context)
        {
            Dictionary<string, int> branchIds = EnsureCanonicalBranches(context);
            Dictionary<string, int> studyYearIds = EnsureCanonicalStudyYears(context);

            MergeDuplicateBranches(context, branchIds);
            MergeDuplicateStudyYears(context, studyYearIds);
            MergeDuplicateSections(context);
        }

        private static Dictionary<string, int> EnsureCanonicalBranches(AppDbContext context)
        {
            string[] names =
            {
                ReferenceNameNormalizer.ComputerScience,
                ReferenceNameNormalizer.CyberSecurity,
                ReferenceNameNormalizer.InformationTechnology
            };

            var result = new Dictionary<string, int>();

            foreach (string name in names)
            {
                Branch? branch = context.Branches
                    .AsEnumerable()
                    .Where(b => ReferenceNameNormalizer.NormalizeBranchName(b.BranchName) == name)
                    .OrderBy(b => b.BranchName == name ? 0 : 1)
                    .ThenBy(b => b.BranchID)
                    .FirstOrDefault();

                if (branch == null)
                {
                    branch = new Branch { BranchName = name };
                    context.Branches.Add(branch);
                    context.SaveChanges();
                }

                branch.BranchName = name;
                result[name] = branch.BranchID;
            }

            context.SaveChanges();
            return result;
        }

        private static Dictionary<string, int> EnsureCanonicalStudyYears(AppDbContext context)
        {
            string[] names =
            {
                ReferenceNameNormalizer.FirstYear,
                ReferenceNameNormalizer.SecondYear,
                ReferenceNameNormalizer.ThirdYear,
                ReferenceNameNormalizer.FourthYear
            };

            var result = new Dictionary<string, int>();

            foreach (string name in names)
            {
                StudyYear? studyYear = context.StudyYears
                    .AsEnumerable()
                    .Where(y => ReferenceNameNormalizer.NormalizeStudyYearName(y.YearName) == name)
                    .OrderBy(y => y.YearName == name ? 0 : 1)
                    .ThenBy(y => y.StudyYearID)
                    .FirstOrDefault();

                if (studyYear == null)
                {
                    studyYear = new StudyYear { YearName = name };
                    context.StudyYears.Add(studyYear);
                    context.SaveChanges();
                }

                studyYear.YearName = name;
                result[name] = studyYear.StudyYearID;
            }

            context.SaveChanges();
            return result;
        }

        private static void MergeDuplicateBranches(AppDbContext context, Dictionary<string, int> branchIds)
        {
            foreach (Branch branch in context.Branches.ToList())
            {
                string canonicalName = ReferenceNameNormalizer.NormalizeBranchName(branch.BranchName);

                if (!branchIds.TryGetValue(canonicalName, out int targetBranchId))
                {
                    continue;
                }

                if (branch.BranchID == targetBranchId)
                {
                    branch.BranchName = canonicalName;
                    continue;
                }

                foreach (Subject subject in context.Subjects.Where(s => s.BranchID == branch.BranchID))
                {
                    subject.BranchID = targetBranchId;
                }

                foreach (Section section in context.Sections.Where(s => s.BranchID == branch.BranchID))
                {
                    section.BranchID = targetBranchId;
                }

                foreach (Schedule schedule in context.Schedules.Where(s => s.BranchID == branch.BranchID))
                {
                    schedule.BranchID = targetBranchId;
                }

                context.Branches.Remove(branch);
            }

            context.SaveChanges();
        }

        private static void MergeDuplicateStudyYears(AppDbContext context, Dictionary<string, int> studyYearIds)
        {
            foreach (StudyYear studyYear in context.StudyYears.ToList())
            {
                string canonicalName = ReferenceNameNormalizer.NormalizeStudyYearName(studyYear.YearName);

                if (!studyYearIds.TryGetValue(canonicalName, out int targetStudyYearId))
                {
                    continue;
                }

                if (studyYear.StudyYearID == targetStudyYearId)
                {
                    studyYear.YearName = canonicalName;
                    continue;
                }

                foreach (Subject subject in context.Subjects.Where(s => s.StudyYearID == studyYear.StudyYearID))
                {
                    subject.StudyYearID = targetStudyYearId;
                }

                foreach (Section section in context.Sections.Where(s => s.StudyYearID == studyYear.StudyYearID))
                {
                    section.StudyYearID = targetStudyYearId;
                }

                foreach (Schedule schedule in context.Schedules.Where(s => s.StudyYearID == studyYear.StudyYearID))
                {
                    schedule.StudyYearID = targetStudyYearId;
                }

                context.StudyYears.Remove(studyYear);
            }

            context.SaveChanges();
        }

        private static void MergeDuplicateSections(AppDbContext context)
        {
            List<Section> sections = context.Sections.ToList();
            var duplicateGroups = sections
                .GroupBy(s => new
                {
                    Name = NormalizeSectionKey(s.SectionName),
                    s.StudyYearID,
                    BranchID = s.BranchID ?? 0
                })
                .Where(group => group.Count() > 1)
                .ToList();

            foreach (var group in duplicateGroups)
            {
                Section keep = group.OrderBy(s => s.SectionID).First();

                foreach (Section duplicate in group.Where(s => s.SectionID != keep.SectionID))
                {
                    foreach (Schedule schedule in context.Schedules.Where(s => s.SectionID == duplicate.SectionID).ToList())
                    {
                        bool sameSlotExists = context.Schedules.Any(s =>
                            s.SectionID == keep.SectionID &&
                            s.DayOfWeek == schedule.DayOfWeek &&
                            s.TimeSlotID == schedule.TimeSlotID &&
                            s.ScheduleID != schedule.ScheduleID);

                        if (sameSlotExists)
                        {
                            context.Schedules.Remove(schedule);
                        }
                        else
                        {
                            schedule.SectionID = keep.SectionID;
                        }
                    }

                    context.Sections.Remove(duplicate);
                }
            }

            context.SaveChanges();
        }

        private static string NormalizeSectionKey(string sectionName)
        {
            return new string(sectionName.Trim().ToLowerInvariant().Where(char.IsLetterOrDigit).ToArray());
        }
    }
}
