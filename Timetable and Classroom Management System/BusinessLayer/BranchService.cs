using Timetable_and_Classroom_Management_System.DataAccessLayer;
using Timetable_and_Classroom_Management_System.Models;

namespace Timetable_and_Classroom_Management_System.BusinessLayer
{
    public class BranchService
    {
        public List<Branch> GetAllBranches()
        {
            using AppDbContext context = new AppDbContext();

            return context.Branches
                .OrderBy(b => b.BranchID)
                .ToList();
        }

        public void AddBranch(string branchName)
        {
            branchName = branchName.Trim();

            if (string.IsNullOrWhiteSpace(branchName))
            {
                throw new Exception("Branch name is required.");
            }

            using AppDbContext context = new AppDbContext();

            bool exists = context.Branches
                .Any(b => b.BranchName == branchName);

            if (exists)
            {
                throw new Exception("Branch name already exists.");
            }

            Branch branch = new Branch
            {
                BranchName = branchName
            };

            context.Branches.Add(branch);
            context.SaveChanges();
        }

        public void UpdateBranch(int branchId, string branchName)
        {
            branchName = branchName.Trim();

            if (branchId <= 0)
            {
                throw new Exception("Please select a branch to update.");
            }

            if (string.IsNullOrWhiteSpace(branchName))
            {
                throw new Exception("Branch name is required.");
            }

            using AppDbContext context = new AppDbContext();

            Branch? branch = context.Branches
                .FirstOrDefault(b => b.BranchID == branchId);

            if (branch == null)
            {
                throw new Exception("Branch not found.");
            }

            bool exists = context.Branches
                .Any(b => b.BranchName == branchName && b.BranchID != branchId);

            if (exists)
            {
                throw new Exception("Branch name already exists.");
            }

            branch.BranchName = branchName;

            context.SaveChanges();
        }

        public void DeleteBranch(int branchId)
        {
            if (branchId <= 0)
            {
                throw new Exception("Please select a branch to delete.");
            }

            using AppDbContext context = new AppDbContext();

            Branch? branch = context.Branches
                .FirstOrDefault(b => b.BranchID == branchId);

            if (branch == null)
            {
                throw new Exception("Branch not found.");
            }

            bool hasRelatedData =
                context.Subjects.Any(s => s.BranchID == branchId) ||
                context.Sections.Any(s => s.BranchID == branchId) ||
                context.Schedules.Any(s => s.BranchID == branchId);

            if (hasRelatedData)
            {
                throw new Exception("This branch cannot be deleted because it is linked to other data.");
            }

            context.Branches.Remove(branch);
            context.SaveChanges();
        }
    }
}