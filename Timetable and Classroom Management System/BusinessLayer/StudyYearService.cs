using Timetable_and_Classroom_Management_System.DataAccessLayer;
using Timetable_and_Classroom_Management_System.Models;

namespace Timetable_and_Classroom_Management_System.BusinessLayer
{
    public class StudyYearService
    {
        public List<StudyYear> GetAllStudyYears()
        {
            using AppDbContext context = new AppDbContext();

            return context.StudyYears
                .OrderBy(y => y.StudyYearID)
                .ToList();
        }

        public void AddStudyYear(string yearName)
        {
            yearName = yearName.Trim();

            if (string.IsNullOrWhiteSpace(yearName))
            {
                throw new Exception("Study year name is required.");
            }

            using AppDbContext context = new AppDbContext();

            bool exists = context.StudyYears
                .Any(y => y.YearName == yearName);

            if (exists)
            {
                throw new Exception("Study year already exists.");
            }

            StudyYear studyYear = new StudyYear
            {
                YearName = yearName
            };

            context.StudyYears.Add(studyYear);
            context.SaveChanges();
        }

        public void UpdateStudyYear(int studyYearId, string yearName)
        {
            yearName = yearName.Trim();

            if (studyYearId <= 0)
            {
                throw new Exception("Please select a study year to update.");
            }

            if (string.IsNullOrWhiteSpace(yearName))
            {
                throw new Exception("Study year name is required.");
            }

            using AppDbContext context = new AppDbContext();

            StudyYear? studyYear = context.StudyYears
                .FirstOrDefault(y => y.StudyYearID == studyYearId);

            if (studyYear == null)
            {
                throw new Exception("Study year not found.");
            }

            bool exists = context.StudyYears
                .Any(y => y.YearName == yearName && y.StudyYearID != studyYearId);

            if (exists)
            {
                throw new Exception("Study year already exists.");
            }

            studyYear.YearName = yearName;
            context.SaveChanges();
        }

        public void DeleteStudyYear(int studyYearId)
        {
            if (studyYearId <= 0)
            {
                throw new Exception("Please select a study year to delete.");
            }

            using AppDbContext context = new AppDbContext();

            StudyYear? studyYear = context.StudyYears
                .FirstOrDefault(y => y.StudyYearID == studyYearId);

            if (studyYear == null)
            {
                throw new Exception("Study year not found.");
            }

            bool hasRelatedData =
                context.Subjects.Any(s => s.StudyYearID == studyYearId) ||
                context.Sections.Any(s => s.StudyYearID == studyYearId) ||
                context.Schedules.Any(s => s.StudyYearID == studyYearId);

            if (hasRelatedData)
            {
                throw new Exception("This study year cannot be deleted because it is linked to other data.");
            }

            context.StudyYears.Remove(studyYear);
            context.SaveChanges();
        }
    }
}
