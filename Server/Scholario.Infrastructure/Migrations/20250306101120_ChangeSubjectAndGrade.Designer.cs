﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Scholario.Infrastructure.Persistence;

#nullable disable

namespace Scholario.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250306101120_ChangeSubjectAndGrade")]
    partial class ChangeSubjectAndGrade
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GroupSubject", b =>
                {
                    b.Property<int>("GroupsId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectsId")
                        .HasColumnType("int");

                    b.HasKey("GroupsId", "SubjectsId");

                    b.HasIndex("SubjectsId");

                    b.ToTable("GroupSubjects", (string)null);
                });

            modelBuilder.Entity("Scholario.Domain.Entities.DescriptiveAssessment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DescriptiveAssessments");
                });

            modelBuilder.Entity("Scholario.Domain.Entities.Grade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfIssue")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DescriptiveAssessmentId")
                        .HasColumnType("int");

                    b.Property<float?>("GradeValue")
                        .HasColumnType("real");

                    b.Property<int>("GradeWeight")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DescriptiveAssessmentId");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("Scholario.Domain.Entities.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId")
                        .IsUnique();

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Scholario.Domain.Entities.LessonHour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<int>("LessonNumber")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("LessonHours");
                });

            modelBuilder.Entity("Scholario.Domain.Entities.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<int>("MessageType")
                        .HasColumnType("int");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Sent")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Scholario.Domain.Entities.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Scholario.Domain.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Persons", (string)null);

                    b.HasDiscriminator<int>("Role").HasValue(0);

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Scholario.Domain.Entities.ScheduleEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("LessonHourId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("LessonHourId");

                    b.HasIndex("SubjectId");

                    b.ToTable("ScheduleEntries");
                });

            modelBuilder.Entity("Scholario.Domain.Entities.StudentAttendance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AttendanceJson")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ScheduleEntryId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleEntryId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentAttendances");
                });

            modelBuilder.Entity("Scholario.Domain.Entities.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FinalAssessment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SemiFinalAssessment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("Scholario.Domain.Entities.Parent", b =>
                {
                    b.HasBaseType("Scholario.Domain.Entities.Person");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue(3);
                });

            modelBuilder.Entity("Scholario.Domain.Entities.Student", b =>
                {
                    b.HasBaseType("Scholario.Domain.Entities.Person");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.HasIndex("GroupId");

                    b.HasIndex("ParentId");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("Scholario.Domain.Entities.Teacher", b =>
                {
                    b.HasBaseType("Scholario.Domain.Entities.Person");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("GroupSubject", b =>
                {
                    b.HasOne("Scholario.Domain.Entities.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scholario.Domain.Entities.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Scholario.Domain.Entities.Grade", b =>
                {
                    b.HasOne("Scholario.Domain.Entities.DescriptiveAssessment", "DescriptiveAssessment")
                        .WithMany("Grades")
                        .HasForeignKey("DescriptiveAssessmentId");

                    b.HasOne("Scholario.Domain.Entities.Student", "Student")
                        .WithMany("Grades")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scholario.Domain.Entities.Subject", "Subject")
                        .WithMany("Grades")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DescriptiveAssessment");

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Scholario.Domain.Entities.Group", b =>
                {
                    b.HasOne("Scholario.Domain.Entities.Teacher", "Teacher")
                        .WithOne("Group")
                        .HasForeignKey("Scholario.Domain.Entities.Group", "TeacherId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Scholario.Domain.Entities.Message", b =>
                {
                    b.HasOne("Scholario.Domain.Entities.Person", "Receiver")
                        .WithMany("ReceivedMessages")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scholario.Domain.Entities.Person", "Sender")
                        .WithMany("SentMessages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Scholario.Domain.Entities.Notification", b =>
                {
                    b.HasOne("Scholario.Domain.Entities.Person", "Receiver")
                        .WithMany("Notifications")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receiver");
                });

            modelBuilder.Entity("Scholario.Domain.Entities.ScheduleEntry", b =>
                {
                    b.HasOne("Scholario.Domain.Entities.Group", "Group")
                        .WithMany("ScheduleEntries")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Scholario.Domain.Entities.LessonHour", "LessonHour")
                        .WithMany("ScheduleEntries")
                        .HasForeignKey("LessonHourId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Scholario.Domain.Entities.Subject", "Subject")
                        .WithMany("ScheduleEntries")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("LessonHour");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Scholario.Domain.Entities.StudentAttendance", b =>
                {
                    b.HasOne("Scholario.Domain.Entities.ScheduleEntry", "ScheduleEntry")
                        .WithMany("StudentAttendances")
                        .HasForeignKey("ScheduleEntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scholario.Domain.Entities.Student", "Student")
                        .WithMany("StudentAttendances")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ScheduleEntry");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Scholario.Domain.Entities.Subject", b =>
                {
                    b.HasOne("Scholario.Domain.Entities.Teacher", "Teacher")
                        .WithMany("Subjects")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Scholario.Domain.Entities.Student", b =>
                {
                    b.HasOne("Scholario.Domain.Entities.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scholario.Domain.Entities.Parent", "Parent")
                        .WithMany("Students")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Scholario.Domain.Entities.DescriptiveAssessment", b =>
                {
                    b.Navigation("Grades");
                });

            modelBuilder.Entity("Scholario.Domain.Entities.Group", b =>
                {
                    b.Navigation("ScheduleEntries");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("Scholario.Domain.Entities.LessonHour", b =>
                {
                    b.Navigation("ScheduleEntries");
                });

            modelBuilder.Entity("Scholario.Domain.Entities.Person", b =>
                {
                    b.Navigation("Notifications");

                    b.Navigation("ReceivedMessages");

                    b.Navigation("SentMessages");
                });

            modelBuilder.Entity("Scholario.Domain.Entities.ScheduleEntry", b =>
                {
                    b.Navigation("StudentAttendances");
                });

            modelBuilder.Entity("Scholario.Domain.Entities.Subject", b =>
                {
                    b.Navigation("Grades");

                    b.Navigation("ScheduleEntries");
                });

            modelBuilder.Entity("Scholario.Domain.Entities.Parent", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Scholario.Domain.Entities.Student", b =>
                {
                    b.Navigation("Grades");

                    b.Navigation("StudentAttendances");
                });

            modelBuilder.Entity("Scholario.Domain.Entities.Teacher", b =>
                {
                    b.Navigation("Group")
                        .IsRequired();

                    b.Navigation("Subjects");
                });
#pragma warning restore 612, 618
        }
    }
}
