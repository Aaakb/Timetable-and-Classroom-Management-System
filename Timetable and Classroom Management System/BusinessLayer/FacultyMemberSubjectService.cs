using Microsoft.EntityFrameworkCore;
using Timetable_and_Classroom_Management_System.DataAccessLayer;
using Timetable_and_Classroom_Management_System.Models;

namespace Timetable_and_Classroom_Management_System.BusinessLayer
{
    public class FacultyMemberSubjectService
    {
        public List<FacultyMemberSubject> GetAllAssignments()
        {
            using AppDbContext context = new AppDbContext();

            return context.FacultyMemberSubjects
                .Include(a => a.FacultyMember)
                .Include(a => a.Subject)
                .OrderBy(a => a.FacultyMember.FullName)
                .ThenBy(a => a.Subject.SubjectName)
                .ToList();
        }

        public void AssignSubject(int facultyMemberId, int subjectId)
        {
            if (facultyMemberId <= 0)
            {
                throw new Exception("Please select a faculty member.");
            }

            if (subjectId <= 0)
            {
                throw new Exception("Please select a subject.");
            }

            using AppDbContext context = new AppDbContext();

            bool facultyMemberExists = context.FacultyMembers
                .Any(f => f.FacultyMemberID == facultyMemberId);

            if (!facultyMemberExists)
            {
                throw new Exception("Selected faculty member does not exist.");
            }

            bool subjectExists = context.Subjects
                .Any(s => s.SubjectID == subjectId);

            if (!subjectExists)
            {
                throw new Exception("Selected subject does not exist.");
            }

            bool exists = context.FacultyMemberSubjects
                .Any(a => a.FacultyMemberID == facultyMemberId && a.SubjectID == subjectId);

            if (exists)
            {
                throw new Exception("This subject is already assigned to the selected faculty member.");
            }

            context.FacultyMemberSubjects.Add(new FacultyMemberSubject
            {
                FacultyMemberID = facultyMemberId,
                SubjectID = subjectId
            });

            context.SaveChanges();
        }

        public void RemoveAssignment(int facultyMemberId, int subjectId)
        {
            if (facultyMemberId <= 0 || subjectId <= 0)
            {
                throw new Exception("Please select an assignment to remove.");
            }

            using AppDbContext context = new AppDbContext();

            FacultyMemberSubject? assignment = context.FacultyMemberSubjects
                .FirstOrDefault(a => a.FacultyMemberID == facultyMemberId && a.SubjectID == subjectId);

            if (assignment == null)
            {
                throw new Exception("Assignment not found.");
            }

            bool hasSchedules = context.Schedules
                .Any(s => s.FacultyMemberID == facultyMemberId && s.SubjectID == subjectId);

            if (hasSchedules)
            {
                throw new Exception("This assignment cannot be removed because it is used in schedules.");
            }

            context.FacultyMemberSubjects.Remove(assignment);
            context.SaveChanges();
        }
    }
}
