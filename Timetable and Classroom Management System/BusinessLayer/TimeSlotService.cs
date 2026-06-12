using Timetable_and_Classroom_Management_System.DataAccessLayer;
using Timetable_and_Classroom_Management_System.Models;

namespace Timetable_and_Classroom_Management_System.BusinessLayer
{
    public class TimeSlotService
    {
        public List<TimeSlot> GetAllTimeSlots()
        {
            using AppDbContext context = new AppDbContext();

            return context.TimeSlots
                .OrderBy(t => t.StartTime)
                .ToList();
        }

        public void AddTimeSlot(TimeSpan startTime, TimeSpan endTime, bool isBreak)
        {
            ValidateTimeRange(startTime, endTime);

            using AppDbContext context = new AppDbContext();

            EnsureNoOverlap(context, startTime, endTime, null);

            TimeSlot timeSlot = new TimeSlot
            {
                StartTime = startTime,
                EndTime = endTime,
                IsBreak = isBreak
            };

            context.TimeSlots.Add(timeSlot);
            context.SaveChanges();
        }

        public void UpdateTimeSlot(int timeSlotId, TimeSpan startTime, TimeSpan endTime, bool isBreak)
        {
            if (timeSlotId <= 0)
            {
                throw new Exception("Please select a time slot to update.");
            }

            ValidateTimeRange(startTime, endTime);

            using AppDbContext context = new AppDbContext();

            TimeSlot? timeSlot = context.TimeSlots
                .FirstOrDefault(t => t.TimeSlotID == timeSlotId);

            if (timeSlot == null)
            {
                throw new Exception("Time slot not found.");
            }

            EnsureNoOverlap(context, startTime, endTime, timeSlotId);

            if (isBreak && context.Schedules.Any(s => s.TimeSlotID == timeSlotId))
            {
                throw new Exception("This time slot cannot be changed to a break because it is used in schedules.");
            }

            timeSlot.StartTime = startTime;
            timeSlot.EndTime = endTime;
            timeSlot.IsBreak = isBreak;

            context.SaveChanges();
        }

        public void DeleteTimeSlot(int timeSlotId)
        {
            if (timeSlotId <= 0)
            {
                throw new Exception("Please select a time slot to delete.");
            }

            using AppDbContext context = new AppDbContext();

            TimeSlot? timeSlot = context.TimeSlots
                .FirstOrDefault(t => t.TimeSlotID == timeSlotId);

            if (timeSlot == null)
            {
                throw new Exception("Time slot not found.");
            }

            bool hasSchedules = context.Schedules
                .Any(s => s.TimeSlotID == timeSlotId);

            if (hasSchedules)
            {
                throw new Exception("This time slot cannot be deleted because it is used in schedules.");
            }

            context.TimeSlots.Remove(timeSlot);
            context.SaveChanges();
        }

        private static void ValidateTimeRange(TimeSpan startTime, TimeSpan endTime)
        {
            if (startTime >= endTime)
            {
                throw new Exception("Start time must be before end time.");
            }
        }

        private static void EnsureNoOverlap(AppDbContext context, TimeSpan startTime, TimeSpan endTime, int? excludedTimeSlotId)
        {
            bool overlaps = context.TimeSlots
                .Any(t =>
                    (!excludedTimeSlotId.HasValue || t.TimeSlotID != excludedTimeSlotId.Value) &&
                    startTime < t.EndTime &&
                    endTime > t.StartTime);

            if (overlaps)
            {
                throw new Exception("Time slot overlaps with an existing time slot.");
            }
        }
    }
}
