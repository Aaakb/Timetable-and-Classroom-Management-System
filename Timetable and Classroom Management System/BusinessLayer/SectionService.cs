using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable_and_Classroom_Management_System.DataAccessLayer;
using Timetable_and_Classroom_Management_System.Models;

namespace Timetable_and_Classroom_Management_System.BusinessLayer
{
    public class SectionService
    {
        public List<Section> GetAllSections()
        {
            using AppDbContext context = new AppDbContext();

            return context.Sections
                .OrderBy(s => s.SectionID)
                .ToList();
        }

        public void AddSection(string sectionName, int studyYearId, int? branchId, int studentCount)
        {
            sectionName = sectionName.Trim();

            if (string.IsNullOrWhiteSpace(sectionName))
            {
                throw new Exception("Section name is required.");
            }

            if (studyYearId <= 0)
            {
                throw new Exception("Please select a study year.");
            }

            if (studentCount <= 0)
            {
                throw new Exception("Student count must be greater than zero.");
            }

            if (branchId.HasValue && branchId.Value <= 0)
            {
                throw new Exception("Please select a branch.");
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

            bool sectionExists = context.Sections
                .Any(s =>
                    s.SectionName == sectionName &&
                    s.StudyYearID == studyYearId &&
                    s.BranchID == branchId);

            if (sectionExists)
            {
                throw new Exception("Section name already exists for this study year and branch.");
            }

            Section section = new Section
            {
                SectionName = sectionName,
                StudyYearID = studyYearId,
                BranchID = branchId,
                StudentCount = studentCount
            };

            context.Sections.Add(section);
            context.SaveChanges();
        }

        public void UpdateSection(int sectionId, string sectionName, int studyYearId, int? branchId, int studentCount)
        {
            sectionName = sectionName.Trim();

            if (sectionId <= 0)
            {
                throw new Exception("Please select a section to update.");
            }

            if (string.IsNullOrWhiteSpace(sectionName))
            {
                throw new Exception("Section name is required.");
            }

            if (studyYearId <= 0)
            {
                throw new Exception("Please select a study year.");
            }

            if (studentCount <= 0)
            {
                throw new Exception("Student count must be greater than zero.");
            }

            if (branchId.HasValue && branchId.Value <= 0)
            {
                throw new Exception("Please select a branch.");
            }

            using AppDbContext context = new AppDbContext();

            Section? section = context.Sections
                .FirstOrDefault(s => s.SectionID == sectionId);

            if (section == null)
            {
                throw new Exception("Section not found.");
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

            bool sectionExists = context.Sections
                .Any(s =>
                    s.SectionName == sectionName &&
                    s.StudyYearID == studyYearId &&
                    s.BranchID == branchId &&
                    s.SectionID != sectionId);

            if (sectionExists)
            {
                throw new Exception("Section name already exists for this study year and branch.");
            }

            section.SectionName = sectionName;
            section.StudyYearID = studyYearId;
            section.BranchID = branchId;
            section.StudentCount = studentCount;

            context.SaveChanges();
        }

        public void DeleteSection(int sectionId)
        {
            if (sectionId <= 0)
            {
                throw new Exception("Please select a section to delete.");
            }

            using AppDbContext context = new AppDbContext();

            Section? section = context.Sections
                .FirstOrDefault(s => s.SectionID == sectionId);

            if (section == null)
            {
                throw new Exception("Section not found.");
            }

            bool hasRelatedData = context.Schedules
                .Any(s => s.SectionID == sectionId);

            if (hasRelatedData)
            {
                throw new Exception("This section cannot be deleted because it is linked to schedules.");
            }

            context.Sections.Remove(section);
            context.SaveChanges();
        }
    }
}
