using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Timetable_and_Classroom_Management_System.Models;

namespace Timetable_and_Classroom_Management_System.DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<FacultyMember> FacultyMembers { get; set; }
        public DbSet<FacultyMemberSubject> FacultyMemberSubjects { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<StudyYear> StudyYears { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }

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