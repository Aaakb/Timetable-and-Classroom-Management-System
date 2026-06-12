using Timetable_and_Classroom_Management_System.DataAccessLayer;
using Timetable_and_Classroom_Management_System.Models;

namespace Timetable_and_Classroom_Management_System.BusinessLayer
{
    public class ClassroomService
    {
        public List<Classroom> GetAllClassrooms()
        {
            using AppDbContext context = new AppDbContext();

            return context.Classrooms
                .OrderBy(c => c.ClassroomID)
                .ToList();
        }

        public void AddClassroom(string classroomNumber, int capacity, string? roomType)
        {
            classroomNumber = classroomNumber.Trim();
            roomType = roomType?.Trim();

            if (string.IsNullOrWhiteSpace(classroomNumber))
            {
                throw new Exception("Classroom number is required.");
            }

            if (capacity <= 0)
            {
                throw new Exception("Capacity must be greater than zero.");
            }

            using AppDbContext context = new AppDbContext();

            bool exists = context.Classrooms
                .Any(c => c.ClassroomNumber == classroomNumber);

            if (exists)
            {
                throw new Exception("Classroom number already exists.");
            }

            Classroom classroom = new Classroom
            {
                ClassroomNumber = classroomNumber,
                Capacity = capacity,
                RoomType = roomType
            };

            context.Classrooms.Add(classroom);
            context.SaveChanges();
        }

        public void UpdateClassroom(int classroomId, string classroomNumber, int capacity, string? roomType)
        {
            classroomNumber = classroomNumber.Trim();
            roomType = roomType?.Trim();

            if (classroomId <= 0)
            {
                throw new Exception("Please select a classroom to update.");
            }

            if (string.IsNullOrWhiteSpace(classroomNumber))
            {
                throw new Exception("Classroom number is required.");
            }

            if (capacity <= 0)
            {
                throw new Exception("Capacity must be greater than zero.");
            }

            using AppDbContext context = new AppDbContext();

            Classroom? classroom = context.Classrooms
                .FirstOrDefault(c => c.ClassroomID == classroomId);

            if (classroom == null)
            {
                throw new Exception("Classroom not found.");
            }

            bool exists = context.Classrooms
                .Any(c => c.ClassroomNumber == classroomNumber && c.ClassroomID != classroomId);

            if (exists)
            {
                throw new Exception("Classroom number already exists.");
            }

            classroom.ClassroomNumber = classroomNumber;
            classroom.Capacity = capacity;
            classroom.RoomType = roomType;

            context.SaveChanges();
        }

        public void DeleteClassroom(int classroomId)
        {
            if (classroomId <= 0)
            {
                throw new Exception("Please select a classroom to delete.");
            }

            using AppDbContext context = new AppDbContext();

            Classroom? classroom = context.Classrooms
                .FirstOrDefault(c => c.ClassroomID == classroomId);

            if (classroom == null)
            {
                throw new Exception("Classroom not found.");
            }

            bool hasSchedules = context.Schedules
                .Any(s => s.ClassroomID == classroomId);

            if (hasSchedules)
            {
                throw new Exception("This classroom cannot be deleted because it is used in schedules.");
            }

            context.Classrooms.Remove(classroom);
            context.SaveChanges();
        }
    }
}