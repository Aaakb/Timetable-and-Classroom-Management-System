using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timetable_and_Classroom_Management_System.Models;

namespace Timetable_and_Classroom_Management_System.Models
{
    public class FacultyMember
    {
        public int FacultyMemberID { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string? AcademicTitle { get; set; }


        // Navigation Properties

        public ICollection<FacultyMemberSubject> FacultyMemberSubjects { get; set; }
            = new List<FacultyMemberSubject>();

        public ICollection<Schedule> Schedules { get; set; }
            = new List<Schedule>();
    }
}