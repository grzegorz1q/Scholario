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
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Parent> Parents { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<DescriptiveAssessment> DescriptiveAssessments { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=DESKTOP-GMLDU51\\SQLEXPRESS;Database=SCHOLARIO_DATABASE;Trusted_Connection=True;TrustServerCertificate=True;");
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
                .HasForeignKey(s => s.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacja Group -> Teacher (jeden do jednego)
            modelBuilder.Entity<Group>()
                .HasOne(g => g.Teacher)
                .WithOne(t => t.Group)
                .HasForeignKey<Teacher>(t => t.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

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
                .OnDelete(DeleteBehavior.Restrict);

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
                .HasForeignKey(g => g.DescriptiveAssessmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacja Message -> Sender (Person)
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacja Message -> Receiver (Person)
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany()
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacja Notification -> Receiver (Person)
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Receiver)
                .WithMany()
                .HasForeignKey(n => n.ReceivertId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacja wiele do wielu Group <-> Subject
            modelBuilder.Entity<Group>()
                .HasMany(g => g.Subjects)
                .WithMany(s => s.Groups)
                .UsingEntity(j => j.ToTable("GroupSubjects"));
        }
    }
}