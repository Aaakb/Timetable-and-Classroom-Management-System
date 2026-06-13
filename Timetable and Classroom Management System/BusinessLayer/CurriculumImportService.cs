using Timetable_and_Classroom_Management_System.DataAccessLayer;
using Timetable_and_Classroom_Management_System.Models;

namespace Timetable_and_Classroom_Management_System.BusinessLayer
{
    public class CurriculumImportService
    {
        public CurriculumImportResult ImportComputerScienceCurriculum()
        {
            using AppDbContext context = new AppDbContext();

            Dictionary<string, StudyYear> studyYears = EnsureStudyYears(context);
            Dictionary<string, Branch> branches = EnsureBranches(context);

            int addedSubjects = 0;
            int updatedSubjects = 0;

            foreach (CurriculumSubject source in Subjects)
            {
                string studyYearName = ReferenceNameNormalizer.NormalizeStudyYearName(source.StudyYear);
                string? branchName = source.BranchName == null
                    ? null
                    : ReferenceNameNormalizer.NormalizeBranchName(source.BranchName);
                int studyYearId = studyYears[studyYearName].StudyYearID;
                int? branchId = branchName == null ? null : branches[branchName].BranchID;

                Subject? subject = context.Subjects.FirstOrDefault(s =>
                    s.SubjectName == source.Name &&
                    s.StudyYearID == studyYearId &&
                    s.SemesterNumber == source.SemesterNumber &&
                    s.BranchID == branchId);

                if (subject == null)
                {
                    subject = new Subject
                    {
                        SubjectName = source.Name,
                        StudyYearID = studyYearId,
                        SemesterNumber = source.SemesterNumber,
                        BranchID = branchId
                    };

                    context.Subjects.Add(subject);
                    addedSubjects++;
                }
                else
                {
                    updatedSubjects++;
                }

                subject.TheoreticalHours = source.TheoreticalHours;
                subject.PracticalHours = source.PracticalHours;
                subject.CreditUnits = source.CreditUnits;
                subject.RequirementType = source.RequirementType;
            }

            context.SaveChanges();

            return new CurriculumImportResult(
                studyYears.Count,
                branches.Count,
                addedSubjects,
                updatedSubjects,
                Subjects.Count);
        }

        private static Dictionary<string, StudyYear> EnsureStudyYears(AppDbContext context)
        {
            var result = new Dictionary<string, StudyYear>();

            foreach (string yearName in StudyYearNames.Select(ReferenceNameNormalizer.NormalizeStudyYearName).Distinct())
            {
                StudyYear? studyYear = context.StudyYears.FirstOrDefault(y => y.YearName == yearName);

                if (studyYear == null)
                {
                    studyYear = new StudyYear
                    {
                        YearName = yearName
                    };

                    context.StudyYears.Add(studyYear);
                }

                result[yearName] = studyYear;
            }

            context.SaveChanges();
            return result;
        }

        private static Dictionary<string, Branch> EnsureBranches(AppDbContext context)
        {
            var result = new Dictionary<string, Branch>();

            foreach (string branchName in BranchNames.Select(ReferenceNameNormalizer.NormalizeBranchName).Distinct())
            {
                Branch? branch = context.Branches.FirstOrDefault(b => b.BranchName == branchName);

                if (branch == null)
                {
                    branch = new Branch
                    {
                        BranchName = branchName
                    };

                    context.Branches.Add(branch);
                }

                result[branchName] = branch;
            }

            context.SaveChanges();
            return result;
        }

        private static readonly string[] StudyYearNames =
        {
            "المرحلة الأولى",
            "المرحلة الثانية",
            "المرحلة الثالثة",
            "المرحلة الرابعة"
        };

        private static readonly string[] BranchNames =
        {
            "علوم الحاسوب",
            "الأمن السيبراني",
            "تكنولوجيا المعلومات"
        };

        private static readonly IReadOnlyList<CurriculumSubject> Subjects = new List<CurriculumSubject>
        {
            new("المرحلة الأولى", null, 1, "أساسيات البرمجة (I)", 3, 2, 4, "قسم"),
            new("المرحلة الأولى", null, 1, "مهارات حاسوب (I)", 3, 2, 4, "قسم"),
            new("المرحلة الأولى", null, 1, "أخلاقيات الحاسوب", 2, 0, 2, "قسم"),
            new("المرحلة الأولى", null, 1, "هياكل متقطعة", 3, 0, 3, "قسم"),
            new("المرحلة الأولى", null, 1, "عربي", 2, 0, 2, "جامعية"),
            new("المرحلة الأولى", null, 1, "حقوق الإنسان", 2, 0, 2, "جامعية"),
            new("المرحلة الأولى", null, 2, "أساسيات البرمجة (II)", 3, 2, 4, "قسم"),
            new("المرحلة الأولى", null, 2, "مهارات حاسوب (II)", 3, 2, 4, "قسم"),
            new("المرحلة الأولى", null, 2, "تصميم منطقي", 3, 2, 4, "قسم"),
            new("المرحلة الأولى", null, 2, "تكنولوجيا المعلومات", 3, 0, 3, "قسم"),
            new("المرحلة الأولى", null, 2, "رياضيات", 2, 0, 2, "كلية"),
            new("المرحلة الأولى", null, 2, "ديمقراطية", 2, 0, 2, "جامعية"),
            new("المرحلة الأولى", null, 2, "اللغة الإنكليزية", 2, 0, 2, "جامعية"),

            new("المرحلة الثانية", null, 1, "هندسة برمجيات", 3, 0, 3, "قسم"),
            new("المرحلة الثانية", null, 1, "برمجة كيانية", 3, 2, 4, "قسم"),
            new("المرحلة الثانية", null, 1, "هياكل بيانات", 3, 2, 4, "قسم"),
            new("المرحلة الثانية", null, 1, "تنظيم الحاسوب", 2, 2, 3, "قسم"),
            new("المرحلة الثانية", null, 1, "التجارة الإلكترونية", 3, 2, 4, "قسم"),
            new("المرحلة الثانية", null, 1, "اللغة الإنكليزية", 2, 0, 2, "جامعية"),
            new("المرحلة الثانية", null, 2, "تصميم وتحليل خوارزميات", 3, 2, 4, "قسم"),
            new("المرحلة الثانية", null, 2, "قواعد بيانات", 3, 2, 4, "قسم"),
            new("المرحلة الثانية", null, 2, "تفاعلية الإنسان مع الحاسبة", 2, 2, 3, "قسم"),
            new("المرحلة الثانية", null, 2, "معمارية الحاسبة", 3, 2, 4, "قسم"),
            new("المرحلة الثانية", null, 2, "النظرية الاحتسابية", 3, 0, 3, "قسم"),
            new("المرحلة الثانية", null, 2, "الإحصاء والاحتمالية", 3, 0, 3, "كلية"),

            new("المرحلة الثالثة", "علوم الحاسوب", 1, "هندسة برمجة كيانية", 3, 2, 4, "قسم"),
            new("المرحلة الثالثة", "علوم الحاسوب", 1, "رسم بالحاسب", 3, 2, 4, "قسم"),
            new("المرحلة الثالثة", "علوم الحاسوب", 1, "مترجمات", 3, 2, 4, "قسم"),
            new("المرحلة الثالثة", "علوم الحاسوب", 1, "ذكاء اصطناعي", 3, 2, 4, "قسم"),
            new("المرحلة الثالثة", "علوم الحاسوب", 1, "تصميم وبرمجة المواقع", 3, 2, 4, "قسم"),
            new("المرحلة الثالثة", "علوم الحاسوب", 2, "أمنية بيانات", 3, 2, 4, "قسم"),
            new("المرحلة الثالثة", "علوم الحاسوب", 2, "شبكات الحاسوب", 3, 2, 4, "قسم"),
            new("المرحلة الثالثة", "علوم الحاسوب", 2, "معالجة صور", 3, 2, 4, "قسم"),
            new("المرحلة الثالثة", "علوم الحاسوب", 2, "أنظمة ذكية", 3, 2, 4, "قسم"),
            new("المرحلة الثالثة", "علوم الحاسوب", 2, "الأنظمة المفتوحة", 3, 2, 4, "قسم"),
            new("المرحلة الثالثة", "علوم الحاسوب", 2, "اللغة الإنكليزية", 2, 0, 2, "جامعية"),

            new("المرحلة الثالثة", "الأمن السيبراني", 1, "إدارة المخاطر", 3, 0, 3, "قسم"),
            new("المرحلة الثالثة", "الأمن السيبراني", 1, "أمنية البيانات والمعلومات", 3, 2, 4, "قسم"),
            new("المرحلة الثالثة", "الأمن السيبراني", 1, "مقدمة في الأمن السيبراني", 3, 0, 3, "قسم"),
            new("المرحلة الثالثة", "الأمن السيبراني", 1, "الشفرات الخبيثة", 3, 2, 4, "قسم"),
            new("المرحلة الثالثة", "الأمن السيبراني", 1, "وسائط متعددة", 3, 2, 4, "قسم"),
            new("المرحلة الثالثة", "الأمن السيبراني", 2, "التشفير الانسيابي والكتلي", 3, 2, 4, "قسم"),
            new("المرحلة الثالثة", "الأمن السيبراني", 2, "الاختراق الأخلاقي", 2, 0, 2, "قسم"),
            new("المرحلة الثالثة", "الأمن السيبراني", 2, "التشفير غير المتناظر", 3, 2, 4, "قسم"),
            new("المرحلة الثالثة", "الأمن السيبراني", 2, "أمنية الوسائط المتعددة", 3, 2, 4, "قسم"),
            new("المرحلة الثالثة", "الأمن السيبراني", 2, "شبكات الحاسوب", 3, 2, 4, "قسم"),
            new("المرحلة الثالثة", "الأمن السيبراني", 2, "اللغة الإنكليزية", 2, 0, 2, "جامعية"),

            new("المرحلة الثالثة", "تكنولوجيا المعلومات", 1, "تنظيم البيانات والنمذجة", 3, 0, 3, "قسم"),
            new("المرحلة الثالثة", "تكنولوجيا المعلومات", 1, "تصميم وبرمجة المواقع", 3, 2, 4, "قسم"),
            new("المرحلة الثالثة", "تكنولوجيا المعلومات", 1, "برمجة تكاملية", 3, 2, 4, "قسم"),
            new("المرحلة الثالثة", "تكنولوجيا المعلومات", 1, "إدارة نظم المؤسسات", 3, 2, 4, "قسم"),
            new("المرحلة الثالثة", "تكنولوجيا المعلومات", 1, "برمجة مرئية", 3, 2, 4, "قسم"),
            new("المرحلة الثالثة", "تكنولوجيا المعلومات", 2, "شبكات الحاسوب", 3, 2, 4, "قسم"),
            new("المرحلة الثالثة", "تكنولوجيا المعلومات", 2, "استعلام قواعد بيانات", 3, 2, 4, "قسم"),
            new("المرحلة الثالثة", "تكنولوجيا المعلومات", 2, "تقانة وأمن المعلومات", 3, 2, 4, "قسم"),
            new("المرحلة الثالثة", "تكنولوجيا المعلومات", 2, "تصميم وبرمجة المواقع المتقدمة", 3, 2, 4, "قسم"),
            new("المرحلة الثالثة", "تكنولوجيا المعلومات", 2, "صيانة نظم المعلومات", 3, 2, 4, "قسم"),
            new("المرحلة الثالثة", "تكنولوجيا المعلومات", 2, "اللغة الإنكليزية", 2, 0, 2, "جامعية"),

            new("المرحلة الرابعة", "علوم الحاسوب", 1, "أمنية الشبكات", 3, 2, 4, "قسم"),
            new("المرحلة الرابعة", "علوم الحاسوب", 1, "حوسبة تطورية", 3, 2, 4, "قسم"),
            new("المرحلة الرابعة", "علوم الحاسوب", 1, "نظم تشغيل", 3, 2, 4, "قسم"),
            new("المرحلة الرابعة", "علوم الحاسوب", 1, "استرجاع المعلومات", 3, 2, 4, "قسم"),
            new("المرحلة الرابعة", "علوم الحاسوب", 1, "حوسبة الموبايل", 3, 2, 4, "قسم"),
            new("المرحلة الرابعة", "علوم الحاسوب", 1, "منهج بحث", 2, 0, 2, "قسم"),
            new("المرحلة الرابعة", "علوم الحاسوب", 2, "الحوسبة السحابية", 3, 2, 4, "قسم"),
            new("المرحلة الرابعة", "علوم الحاسوب", 2, "وسائط متعددة", 3, 2, 4, "قسم"),
            new("المرحلة الرابعة", "علوم الحاسوب", 2, "الوكيل الذكي", 3, 2, 4, "قسم"),
            new("المرحلة الرابعة", "علوم الحاسوب", 2, "اللغة الإنكليزية", 2, 0, 2, "قسم"),
            new("المرحلة الرابعة", "علوم الحاسوب", 2, "مشروع بحث", 2, 0, 2, "قسم"),

            new("المرحلة الرابعة", "الأمن السيبراني", 1, "تعلم الماكنة للأمن السيبراني", 3, 2, 4, "قسم"),
            new("المرحلة الرابعة", "الأمن السيبراني", 1, "أمنية الشبكات", 3, 2, 4, "قسم"),
            new("المرحلة الرابعة", "الأمن السيبراني", 1, "التخويل والتحكم بالوصول", 3, 2, 4, "قسم"),
            new("المرحلة الرابعة", "الأمن السيبراني", 1, "أمنية الحوسبة السحابية", 3, 2, 4, "قسم"),
            new("المرحلة الرابعة", "الأمن السيبراني", 1, "أمنية المواقع الإلكترونية", 3, 2, 4, "قسم"),
            new("المرحلة الرابعة", "الأمن السيبراني", 1, "منهج بحث", 2, 0, 2, "قسم"),
            new("المرحلة الرابعة", "الأمن السيبراني", 2, "إخفاء المعلومات والعلامات المائية", 3, 2, 4, "قسم"),
            new("المرحلة الرابعة", "الأمن السيبراني", 2, "أمنية قواعد البيانات", 3, 2, 4, "قسم"),
            new("المرحلة الرابعة", "الأمن السيبراني", 2, "الأدلة الجنائية الرقمية", 3, 2, 4, "قسم"),
            new("المرحلة الرابعة", "الأمن السيبراني", 2, "نظم التشغيل", 3, 2, 4, "قسم"),
            new("المرحلة الرابعة", "الأمن السيبراني", 2, "اللغة الإنكليزية", 2, 0, 2, "جامعية"),
            new("المرحلة الرابعة", "الأمن السيبراني", 2, "مشروع تخرج", 2, 0, 2, "قسم"),

            new("المرحلة الرابعة", "تكنولوجيا المعلومات", 1, "نظم تشغيل", 3, 2, 4, "قسم"),
            new("المرحلة الرابعة", "تكنولوجيا المعلومات", 1, "تعدين بيانات", 3, 2, 4, "قسم"),
            new("المرحلة الرابعة", "تكنولوجيا المعلومات", 1, "حوسبة موبايل", 3, 2, 4, "قسم"),
            new("المرحلة الرابعة", "تكنولوجيا المعلومات", 1, "نظم المعلومات الموزعة", 3, 0, 3, "قسم"),
            new("المرحلة الرابعة", "تكنولوجيا المعلومات", 1, "منهج بحث", 2, 0, 2, "قسم"),
            new("المرحلة الرابعة", "تكنولوجيا المعلومات", 2, "الأدلة الجنائية الإلكترونية", 3, 2, 4, "قسم"),
            new("المرحلة الرابعة", "تكنولوجيا المعلومات", 2, "حوسبة سحابية", 3, 2, 4, "قسم"),
            new("المرحلة الرابعة", "تكنولوجيا المعلومات", 2, "وسائط متعددة", 3, 2, 4, "قسم"),
            new("المرحلة الرابعة", "تكنولوجيا المعلومات", 2, "أمنية معلومات", 3, 2, 4, "قسم"),
            new("المرحلة الرابعة", "تكنولوجيا المعلومات", 2, "اللغة الإنكليزية", 2, 0, 2, "جامعية"),
            new("المرحلة الرابعة", "تكنولوجيا المعلومات", 2, "مشروع بحث", 2, 0, 2, "قسم")
        };

        private sealed record CurriculumSubject(
            string StudyYear,
            string? BranchName,
            int SemesterNumber,
            string Name,
            int TheoreticalHours,
            int PracticalHours,
            int CreditUnits,
            string RequirementType);
    }

    public sealed class CurriculumImportResult
    {
        public CurriculumImportResult(
            int studyYearCount,
            int branchCount,
            int addedSubjectCount,
            int updatedSubjectCount,
            int totalSubjectCount)
        {
            StudyYearCount = studyYearCount;
            BranchCount = branchCount;
            AddedSubjectCount = addedSubjectCount;
            UpdatedSubjectCount = updatedSubjectCount;
            TotalSubjectCount = totalSubjectCount;
        }

        public int StudyYearCount { get; }

        public int BranchCount { get; }

        public int AddedSubjectCount { get; }

        public int UpdatedSubjectCount { get; }

        public int TotalSubjectCount { get; }
    }
}
