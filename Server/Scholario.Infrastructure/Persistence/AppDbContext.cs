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
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>()
                .ToTable("Person")
                .HasDiscriminator<Role>("Role")
                .HasValue<Student>(Role.Student)
                .HasValue<Teacher>(Role.Teacher)
                .HasValue<Parent>(Role.Parent);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Parent)
                .WithMany(p => p.Students)
                .HasForeignKey(s => s.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Group)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.GroupId);

            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Teacher)
                .WithMany(t => t.Subjects)
                .HasForeignKey(s => s.TeacherId);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.StudentId);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Subject)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.SubjectId);

            modelBuilder.Entity<Grade>()
               .HasOne(g => g.DescriptiveAssessment)
               .WithMany(d => d.Grades)
               .HasForeignKey(g => g.DescriptiveAssessmentId);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany()
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Receiver)
                .WithMany()
                .HasForeignKey(n => n.ReceivertId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
