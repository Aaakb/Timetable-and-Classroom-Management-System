using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timetable_and_Classroom_Management_System.Models
{
    public class FacultyMemberSubject
    {
        public int FacultyMemberID { get; set; }

        public int SubjectID { get; set; }


        // Navigation Properties

        public FacultyMember FacultyMember { get; set; } = null!;

        public Subject Subject { get; set; } = null!;
    }
}