using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable_and_Classroom_Management_System.Models
{
    public class TimeSlot
    {
        public int TimeSlotID { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public bool IsBreak { get; set; }


        // Navigation Properties

        public ICollection<Schedule> Schedules { get; set; }
            = new List<Schedule>();
    }
}
