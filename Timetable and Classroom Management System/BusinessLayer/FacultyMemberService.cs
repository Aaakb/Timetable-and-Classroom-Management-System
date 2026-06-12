using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable_and_Classroom_Management_System.DataAccessLayer;
using Timetable_and_Classroom_Management_System.Models;

namespace Timetable_and_Classroom_Management_System.BusinessLayer
{
    public class FacultyMemberService
    {
        public List<FacultyMember> GetAllFacultyMembers()
        {
            using AppDbContext context = new AppDbContext();

            return context.FacultyMembers
                .OrderBy(f => f.FacultyMemberID)
                .ToList();
        }

        public void AddFacultyMember(string fullName, string? academicTitle)
        {
            fullName = fullName.Trim();
            academicTitle = academicTitle?.Trim();

            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new Exception("Faculty member name is required.");
            }

            using AppDbContext context = new AppDbContext();

            bool exists = context.FacultyMembers
                .Any(f => f.FullName == fullName);

            if (exists)
            {
                throw new Exception("Faculty member name already exists.");
            }

            FacultyMember facultyMember = new FacultyMember
            {
                FullName = fullName,
                AcademicTitle = academicTitle
            };

            context.FacultyMembers.Add(facultyMember);
            context.SaveChanges();
        }

        public void UpdateFacultyMember(int facultyMemberId, string fullName, string? academicTitle)
        {
            fullName = fullName.Trim();
            academicTitle = academicTitle?.Trim();

            if (facultyMemberId <= 0)
            {
                throw new Exception("Please select a faculty member to update.");
            }

            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new Exception("Faculty member name is required.");
            }

            using AppDbContext context = new AppDbContext();

            FacultyMember? facultyMember = context.FacultyMembers
                .FirstOrDefault(f => f.FacultyMemberID == facultyMemberId);

            if (facultyMember == null)
            {
                throw new Exception("Faculty member not found.");
            }

            bool exists = context.FacultyMembers
                .Any(f => f.FullName == fullName && f.FacultyMemberID != facultyMemberId);

            if (exists)
            {
                throw new Exception("Faculty member name already exists.");
            }

            facultyMember.FullName = fullName;
            facultyMember.AcademicTitle = academicTitle;

            context.SaveChanges();
        }

        public void DeleteFacultyMember(int facultyMemberId)
        {
            if (facultyMemberId <= 0)
            {
                throw new Exception("Please select a faculty member to delete.");
            }

            using AppDbContext context = new AppDbContext();

            FacultyMember? facultyMember = context.FacultyMembers
                .FirstOrDefault(f => f.FacultyMemberID == facultyMemberId);

            if (facultyMember == null)
            {
                throw new Exception("Faculty member not found.");
            }

            bool hasSubjects = context.FacultyMemberSubjects
                .Any(fms => fms.FacultyMemberID == facultyMemberId);

            bool hasSchedules = context.Schedules
                .Any(s => s.FacultyMemberID == facultyMemberId);

            if (hasSubjects || hasSchedules)
            {
                throw new Exception("This faculty member cannot be deleted because it is linked to subjects or schedules.");
            }

            context.FacultyMembers.Remove(facultyMember);
            context.SaveChanges();
        }
    }
}