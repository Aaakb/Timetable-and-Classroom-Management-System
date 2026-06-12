using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Timetable_and_Classroom_Management_System.Models
{
    public class Subject
    {
        public int SubjectID { get; set; }

        public string SubjectName { get; set; } = string.Empty;

        public int StudyYearID { get; set; }

        public int SemesterNumber { get; set; }

        public int TheoreticalHours { get; set; }

        public int PracticalHours { get; set; }

        public int CreditUnits { get; set; }

        public string RequirementType { get; set; } = string.Empty;

        public int? BranchID { get; set; }


        // Navigation Properties

        public StudyYear StudyYear { get; set; } = null!;

        public Branch? Branch { get; set; }

        public ICollection<FacultyMemberSubject> FacultyMemberSubjects { get; set; }
            = new List<FacultyMemberSubject>();

        public ICollection<Schedule> Schedules { get; set; }
            = new List<Schedule>();
    }
}