using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scholario.Domain.Entities;

namespace Scholario.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<DescriptiveAssessment> DescriptiveAssessments { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=SCHOLARIO_DATABASE;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfiguracja dziedziczenia dla Person
            modelBuilder.Entity<Person>()
                .ToTable("Persons")
                .HasDiscriminator<Role>("Role")
                .HasValue<Student>(Role.Student)
                .HasValue<Teacher>(Role.Teacher)
                .HasValue<Parent>(Role.Parent);

            // Relacja Student -> Parent
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Parent)
                .WithMany(p => p.Students)
                .HasForeignKey(s => s.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacja Student -> Group
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Group)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.GroupId);

            // Relacja Group -> Teacher (jeden do jednego)
            modelBuilder.Entity<Group>()
                .HasOne(g => g.Teacher)
                .WithOne(t => t.Group)
                .HasForeignKey<Teacher>(t => t.GroupId);

            // Relacja Subject -> Teacher
            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Teacher)
                .WithMany(t => t.Subjects)
                .HasForeignKey(s => s.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacja Grade -> Student
            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacja Grade -> Subject
            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Subject)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacja Grade -> DescriptiveAssessment
            modelBuilder.Entity<Grade>()
                .HasOne(g => g.DescriptiveAssessment)
                .WithMany(d => d.Grades)
                .HasForeignKey(g => g.DescriptiveAssessmentId);

            // Relacja Message -> Sender (Person)
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(s => s.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacja Message -> Receiver (Person)
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(r => r.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId);

            // Relacja Notification -> Receiver (Person)
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Receiver)
                .WithMany( r=> r.Notifications)
                .HasForeignKey(n => n.ReceiverId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacja wiele do wielu Group <-> Subject
            modelBuilder.Entity<Group>()
                .HasMany(g => g.Subjects)
                .WithMany(s => s.Groups)
                .UsingEntity(j => j.ToTable("GroupSubjects"));

            // Relacja ScheduleEntry -> Subject
            modelBuilder.Entity<ScheduleEntry>()
                .HasOne(se => se.Subject)
                .WithMany(s => s.ScheduleEntries)
                .HasForeignKey(se => se.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacja ScheduleEntry -> Group
            modelBuilder.Entity<ScheduleEntry>()
                .HasOne(se => se.Group)
                .WithMany(g => g.ScheduleEntries)
                .HasForeignKey(se => se.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacja ScheduleEntry -> Teacher
            modelBuilder.Entity<ScheduleEntry>()
                .HasOne(se => se.Teacher)
                .WithMany(t => t.ScheduleEntries)
                .HasForeignKey(se => se.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacja ScheduleEntry -> LessonHour
            modelBuilder.Entity<ScheduleEntry>()
                .HasOne(se => se.LessonHour)
                .WithOne(lh => lh.ScheduleEntry)
                .HasForeignKey<ScheduleEntry>(se => se.LessonHourId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
