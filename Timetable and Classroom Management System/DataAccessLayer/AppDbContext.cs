using Microsoft.EntityFrameworkCore;
using Timetable_and_Classroom_Management_System.Models;

namespace Timetable_and_Classroom_Management_System.DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public DbSet<Branch> Branches { get; set; } = null!;
        public DbSet<Classroom> Classrooms { get; set; } = null!;
        public DbSet<FacultyMember> FacultyMembers { get; set; } = null!;
        public DbSet<FacultyMemberSubject> FacultyMemberSubjects { get; set; } = null!;
        public DbSet<Schedule> Schedules { get; set; } = null!;
        public DbSet<Section> Sections { get; set; } = null!;
        public DbSet<StudyYear> StudyYears { get; set; } = null!;
        public DbSet<Subject> Subjects { get; set; } = null!;
        public DbSet<TimeSlot> TimeSlots { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=localhost\SQLEXPRESS;Database=UniversityTimetableDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>().ToTable("Branches");
            modelBuilder.Entity<Classroom>().ToTable("Classrooms");
            modelBuilder.Entity<FacultyMember>().ToTable("FacultyMembers");
            modelBuilder.Entity<FacultyMemberSubject>().ToTable("FacultyMemberSubjects");
            modelBuilder.Entity<Schedule>().ToTable("Schedules");
            modelBuilder.Entity<Section>().ToTable("Sections");
            modelBuilder.Entity<StudyYear>().ToTable("StudyYears");
            modelBuilder.Entity<Subject>().ToTable("Subjects");
            modelBuilder.Entity<TimeSlot>().ToTable("TimeSlots");

            modelBuilder.Entity<FacultyMemberSubject>()
                .HasKey(fms => new { fms.FacultyMemberID, fms.SubjectID });

            modelBuilder.Entity<Schedule>()
                .HasIndex(s => new { s.ClassroomID, s.DayOfWeek, s.TimeSlotID })
                .IsUnique()
                .HasDatabaseName("UQ_Classroom_Time");

            modelBuilder.Entity<Schedule>()
                .HasIndex(s => new { s.FacultyMemberID, s.DayOfWeek, s.TimeSlotID })
                .IsUnique()
                .HasDatabaseName("UQ_Faculty_Time");

            modelBuilder.Entity<Schedule>()
                .HasIndex(s => new { s.SectionID, s.DayOfWeek, s.TimeSlotID })
                .IsUnique()
                .HasFilter("[SectionID] IS NOT NULL")
                .HasDatabaseName("UQ_Section_Time");

            modelBuilder.Entity<FacultyMemberSubject>()
                .HasOne(fms => fms.FacultyMember)
                .WithMany(fm => fm.FacultyMemberSubjects)
                .HasForeignKey(fms => fms.FacultyMemberID);

            modelBuilder.Entity<FacultyMemberSubject>()
                .HasOne(fms => fms.Subject)
                .WithMany(s => s.FacultyMemberSubjects)
                .HasForeignKey(fms => fms.SubjectID);

            modelBuilder.Entity<Subject>()
                .HasOne(s => s.StudyYear)
                .WithMany(sy => sy.Subjects)
                .HasForeignKey(s => s.StudyYearID);

            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Branch)
                .WithMany(b => b.Subjects)
                .HasForeignKey(s => s.BranchID);

            modelBuilder.Entity<Section>()
                .HasOne(s => s.StudyYear)
                .WithMany(sy => sy.Sections)
                .HasForeignKey(s => s.StudyYearID);

            modelBuilder.Entity<Section>()
                .HasOne(s => s.Branch)
                .WithMany(b => b.Sections)
                .HasForeignKey(s => s.BranchID);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Subject)
                .WithMany(sub => sub.Schedules)
                .HasForeignKey(s => s.SubjectID);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.FacultyMember)
                .WithMany(fm => fm.Schedules)
                .HasForeignKey(s => s.FacultyMemberID);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Classroom)
                .WithMany(c => c.Schedules)
                .HasForeignKey(s => s.ClassroomID);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.TimeSlot)
                .WithMany(t => t.Schedules)
                .HasForeignKey(s => s.TimeSlotID);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.StudyYear)
                .WithMany(sy => sy.Schedules)
                .HasForeignKey(s => s.StudyYearID);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Branch)
                .WithMany(b => b.Schedules)
                .HasForeignKey(s => s.BranchID);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Section)
                .WithMany(sec => sec.Schedules)
                .HasForeignKey(s => s.SectionID);
        }
    }
}
