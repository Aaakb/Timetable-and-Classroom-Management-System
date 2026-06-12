using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Timetable_and_Classroom_Management_System.Models
{
    public class Section
    {
        public int SectionID { get; set; }

        public string SectionName { get; set; } = string.Empty;

        public int StudentCount { get; set; }

        public int StudyYearID { get; set; }

        public int? BranchID { get; set; }


        // Navigation Properties

        public StudyYear StudyYear { get; set; } = null!;

        public Branch? Branch { get; set; }

        public ICollection<Schedule> Schedules { get; set; }
            = new List<Schedule>();
    }
}

