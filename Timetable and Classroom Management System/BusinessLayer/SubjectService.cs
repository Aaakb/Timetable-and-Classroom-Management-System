using Timetable_and_Classroom_Management_System.DataAccessLayer;
using Timetable_and_Classroom_Management_System.Models;

namespace Timetable_and_Classroom_Management_System.BusinessLayer
{
    public class SubjectService
    {
        public List<Subject> GetAllSubjects()
        {
            using AppDbContext context = new AppDbContext();

            return context.Subjects
                .OrderBy(s => s.SubjectID)
                .ToList();
        }

        public void AddSubject(
            string subjectName,
            int studyYearId,
            int semesterNumber,
            double theoreticalHours,
            double practicalHours,
            double creditUnits,
            int? branchId)
        {
            subjectName = subjectName.Trim();

            if (string.IsNullOrWhiteSpace(subjectName))
            {
                throw new Exception("Subject name is required.");
            }

            if (studyYearId <= 0)
            {
                throw new Exception("Please select a study year.");
            }

            if (semesterNumber != 1 && semesterNumber != 2)
            {
                throw new Exception("Semester number must be 1 or 2.");
            }

            if (theoreticalHours < 0)
            {
                throw new Exception("Theoretical hours cannot be negative.");
            }

            if (practicalHours < 0)
            {
                throw new Exception("Practical hours cannot be negative.");
            }

            if (creditUnits <= 0)
            {
                throw new Exception("Credit units must be greater than zero.");
            }

            using AppDbContext context = new AppDbContext();

            bool studyYearExists = context.StudyYears
                .Any(y => y.StudyYearID == studyYearId);

            if (!studyYearExists)
            {
                throw new Exception("Selected study year does not exist.");
            }

            if (branchId.HasValue)
            {
                bool branchExists = context.Branches
                    .Any(b => b.BranchID == branchId.Value);

                if (!branchExists)
                {
                    throw new Exception("Selected branch does not exist.");
                }
            }

            bool subjectExists = context.Subjects
                .Any(s =>
                    s.SubjectName == subjectName &&
                    s.StudyYearID == studyYearId &&
                    s.SemesterNumber == semesterNumber &&
                    s.BranchID == branchId);

            if (subjectExists)
            {
                throw new Exception("Subject already exists for this study year, semester, and branch.");
            }

            Subject subject = new Subject
            {
                SubjectName = subjectName,
                StudyYearID = studyYearId,
                SemesterNumber = semesterNumber,
                TheoreticalHours = theoreticalHours,
                PracticalHours = practicalHours,
                CreditUnits = creditUnits,
                RequirementType = string.Empty,
                BranchID = branchId
            };

            context.Subjects.Add(subject);
            context.SaveChanges();
        }

        public void UpdateSubject(
            int subjectId,
            string subjectName,
            int studyYearId,
            int semesterNumber,
            double theoreticalHours,
            double practicalHours,
            double creditUnits,
            int? branchId)
        {
            subjectName = subjectName.Trim();

            if (subjectId <= 0)
            {
                throw new Exception("Please select a subject to update.");
            }

            if (string.IsNullOrWhiteSpace(subjectName))
            {
                throw new Exception("Subject name is required.");
            }

            if (studyYearId <= 0)
            {
                throw new Exception("Please select a study year.");
            }

            if (semesterNumber != 1 && semesterNumber != 2)
            {
                throw new Exception("Semester number must be 1 or 2.");
            }

            if (theoreticalHours < 0)
            {
                throw new Exception("Theoretical hours cannot be negative.");
            }

            if (practicalHours < 0)
            {
                throw new Exception("Practical hours cannot be negative.");
            }

            if (creditUnits <= 0)
            {
                throw new Exception("Credit units must be greater than zero.");
            }

            using AppDbContext context = new AppDbContext();

            Subject? subject = context.Subjects
                .FirstOrDefault(s => s.SubjectID == subjectId);

            if (subject == null)
            {
                throw new Exception("Subject not found.");
            }

            bool studyYearExists = context.StudyYears
                .Any(y => y.StudyYearID == studyYearId);

            if (!studyYearExists)
            {
                throw new Exception("Selected study year does not exist.");
            }

            if (branchId.HasValue)
            {
                bool branchExists = context.Branches
                    .Any(b => b.BranchID == branchId.Value);

                if (!branchExists)
                {
                    throw new Exception("Selected branch does not exist.");
                }
            }

            bool subjectExists = context.Subjects
                .Any(s =>
                    s.SubjectName == subjectName &&
                    s.StudyYearID == studyYearId &&
                    s.SemesterNumber == semesterNumber &&
                    s.BranchID == branchId &&
                    s.SubjectID != subjectId);

            if (subjectExists)
            {
                throw new Exception("Subject already exists for this study year, semester, and branch.");
            }

            subject.SubjectName = subjectName;
            subject.StudyYearID = studyYearId;
            subject.SemesterNumber = semesterNumber;
            subject.TheoreticalHours = theoreticalHours;
            subject.PracticalHours = practicalHours;
            subject.CreditUnits = creditUnits;
            subject.BranchID = branchId;

            context.SaveChanges();
        }

        public void DeleteSubject(int subjectId)
        {
            if (subjectId <= 0)
            {
                throw new Exception("Please select a subject to delete.");
            }

            using AppDbContext context = new AppDbContext();

            Subject? subject = context.Subjects
                .FirstOrDefault(s => s.SubjectID == subjectId);

            if (subject == null)
            {
                throw new Exception("Subject not found.");
            }

            bool hasFacultyMembers = context.FacultyMemberSubjects
                .Any(f => f.SubjectID == subjectId);

            bool hasSchedules = context.Schedules
                .Any(s => s.SubjectID == subjectId);

            if (hasFacultyMembers || hasSchedules)
            {
                throw new Exception("This subject cannot be deleted because it is linked to other data.");
            }

            context.Subjects.Remove(subject);
            context.SaveChanges();
        }
    }
}
