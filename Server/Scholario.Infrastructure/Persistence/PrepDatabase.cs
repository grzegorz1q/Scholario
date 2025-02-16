using Microsoft.AspNetCore.Identity;
using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Infrastructure.Persistence
{
    public class PrepDatabase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPasswordHasher<Person> _passwordHasher;
        public PrepDatabase(AppDbContext appDbContext, IPasswordHasher<Person> passwordHasher)
        {
            _appDbContext = appDbContext;
            _passwordHasher = passwordHasher;
        }
        public void Seed()
        {
            if (_appDbContext.Database.CanConnect())
            {
                if (!_appDbContext.Persons.Any())
                {
                    var admins = GetPersons();
                    _appDbContext.Persons.AddRange(admins);
                    _appDbContext.SaveChanges();
                }
            }
            if (_appDbContext.Database.CanConnect())
            {
                if (!_appDbContext.Persons.OfType<Teacher>().Any())
                {
                    var teachers = GetTeachers();
                    _appDbContext.Persons.AddRange(teachers);
                    _appDbContext.SaveChanges();
                }
            }
            if (_appDbContext.Database.CanConnect())
            {
                if (!_appDbContext.Persons.OfType<Parent>().Any())
                {
                    var parents = GetParents();
                    _appDbContext.Persons.AddRange(parents);
                    _appDbContext.SaveChanges();
                }
            }
            if (_appDbContext.Database.CanConnect())
            {
                if (!_appDbContext.Groups.Any())
                {
                    var groups = GetGroups();
                    _appDbContext.Groups.AddRange(groups);
                    _appDbContext.SaveChanges();
                }
            }
            if (_appDbContext.Database.CanConnect())
            {
                if (!_appDbContext.Persons.OfType<Student>().Any())
                {
                    var students = GetStudents();
                    _appDbContext.Persons.AddRange(students);
                    _appDbContext.SaveChanges();
                }
            }
            if (_appDbContext.Database.CanConnect())
            {
                if (!_appDbContext.Subjects.Any())
                {
                    var subjects = GetSubjects();
                    _appDbContext.Subjects.AddRange(subjects);
                    _appDbContext.SaveChanges();
                }
            }
            if (_appDbContext.Database.CanConnect())
            { 
                if (!_appDbContext.Grades.Any())
                {
                    var grades = GetGrades();
                    _appDbContext.Grades.AddRange(grades);
                    _appDbContext.SaveChanges();
                }
            }
            if (_appDbContext.Database.CanConnect())
            {
                if (!_appDbContext.LessonHours.Any())
                {
                    var lessonHours = GetLessonHours();
                    _appDbContext.LessonHours.AddRange(lessonHours);
                    _appDbContext.SaveChanges();
                }
            }
            if (_appDbContext.Database.CanConnect())
            {
                if (!_appDbContext.ScheduleEntries.Any())
                {
                    var scheduleEntries = GetScheduleEntries();
                    _appDbContext.ScheduleEntries.AddRange(scheduleEntries);
                    _appDbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Teacher> GetTeachers()
        {
            var teachers = new List<Teacher>()
            {
                new Teacher(){FirstName="Jan", LastName="Kowlaski", Email="jan.kowalski@test.pl", Password="jankowalski"},
                new Teacher(){FirstName="Anna", LastName="Maliszewska", Email="anna.maliszewska@test.pl", Password="annamaliszewska"},
            };
            foreach(var e in teachers)
            {
                e.Password = _passwordHasher.HashPassword(e, e.Password);
            }
            return teachers;
        }
        private IEnumerable<Parent> GetParents()
        {
            var parents = new List<Parent>()
            {
                new Parent(){FirstName="Edyta", LastName="Bąk", Email="edyta.bak@test.pl", Password="edytabak", PhoneNumber="987654321", Address="Topolowa 1"},
                new Parent(){FirstName="Karol", LastName="Kot", Email="karol.kot@test.pl", Password="karolkot", PhoneNumber="123456789", Address="Akacjowa 1"}
            };
            foreach (var e in parents)
            {
                e.Password = _passwordHasher.HashPassword(e, e.Password);
            }
            return parents;
        }
        private IEnumerable<Student> GetStudents()
        {
            var parentId = _appDbContext.Persons.OfType<Parent>().First().Id;
            var parentId1 = _appDbContext.Persons.OfType<Parent>().Skip(1).First().Id;
            var groupId = _appDbContext.Groups.First().Id;
            var groupId1 = _appDbContext.Groups.Skip(1).First().Id;
            var students = new List<Student>()
            {
                new Student(){FirstName="Adam", LastName="Nowak", Email="adam.nowak@test.pl", Password="adamnowak", GroupId=groupId, ParentId=parentId},
                new Student(){FirstName="Ala", LastName="Nowakowska", Email="ala.nowakowska@test.pl", Password="alanowakswska", GroupId=groupId, ParentId=parentId},
                new Student(){FirstName="Ada", LastName="Kowalska", Email="ada.kowalska@test.pl", Password="adakowalska", GroupId=groupId1, ParentId=parentId1},
                
            };
            foreach (var e in students)
            {
                e.Password = _passwordHasher.HashPassword(e, e.Password);
            }
            return students;
        }
        private IEnumerable<Person> GetPersons()
        {
            var admins = new List<Person>()
            {
                new Person(){FirstName="admin", LastName="admin", Email="admin@test.pl", Password="admin"}
            };
            foreach (var e in admins)
            {
                e.Password = _passwordHasher.HashPassword(e, e.Password);
            }
            return admins;
        }
        private IEnumerable<Group> GetGroups()
        {
            var teacherId = _appDbContext.Persons.OfType<Teacher>().First().Id;
            var teacherId1 = _appDbContext.Persons.OfType<Teacher>().Skip(1).First().Id;
            var groups = new List<Group>()
            {
                new Group() {Name = "1tb1", TeacherId = teacherId},
                new Group() {Name = "2ti2", TeacherId = teacherId1},

            };
            return groups;
        }
        private IEnumerable<Subject> GetSubjects()
        {
            var teacherId = _appDbContext.Persons.OfType<Teacher>().First().Id;
            var teacherId1 = _appDbContext.Persons.OfType<Teacher>().Skip(1).First().Id;
            var groups = _appDbContext.Groups.ToList();
            var subjects = new List<Subject>()
            {
                new Subject(){ Name="Matematyka", TeacherId=teacherId1, Description="Przedmiot pozwalający zrozumieć logiczne myślenie", Groups = groups },
                new Subject(){ Name="Język polski", TeacherId=teacherId, Description="Nauak o języku", Groups = groups }
            };
            return subjects;
        }
        private IEnumerable<Grade> GetGrades()
        {
            var studentId = _appDbContext.Persons.OfType<Student>().First().Id;
            var subjectId = _appDbContext.Subjects.First().Id;

            var grades = new List<Grade>()
            {

                new Grade()
                {
                    GradeValue = 4.5f,
                    StudentId = studentId,
                    SubjectId = subjectId,
                    DateOfIssue = DateTime.Now
                },

                new Grade()
                {
                    GradeValue = 5.0f,
                    StudentId = studentId,
                    SubjectId = subjectId,
                    DateOfIssue = DateTime.Now
                }
            };

            return grades;
        }
        private IEnumerable<LessonHour> GetLessonHours()
        {
            var lessonHours = new List<LessonHour>()
            {
                new LessonHour() {StartTime = new TimeSpan(8,0,0), EndTime= new TimeSpan(8,45,0), LessonNumber=1},
                new LessonHour() {StartTime = new TimeSpan(8,55,0), EndTime= new TimeSpan(9,40,0), LessonNumber=2},
                new LessonHour() {StartTime = new TimeSpan(9,50,0), EndTime= new TimeSpan(10,35,0), LessonNumber=3},
                new LessonHour() {StartTime = new TimeSpan(10,45,0), EndTime= new TimeSpan(11,30,0), LessonNumber=4},
                new LessonHour() {StartTime = new TimeSpan(11,40,0), EndTime= new TimeSpan(12,25,0), LessonNumber=5},
                new LessonHour() {StartTime = new TimeSpan(12,35,0), EndTime= new TimeSpan(13,20,0), LessonNumber=6},
                new LessonHour() {StartTime = new TimeSpan(13,30,0), EndTime= new TimeSpan(14,15,0), LessonNumber=7},
                new LessonHour() {StartTime = new TimeSpan(14,25,0), EndTime= new TimeSpan(15,10,0), LessonNumber=8}
            };
            return lessonHours;
        }
        private IEnumerable<ScheduleEntry> GetScheduleEntries()
        {
            var group = _appDbContext.Groups.First();
            var group1 = _appDbContext.Groups.Skip(1).First();
            var subject1 = _appDbContext.Subjects.First();
            var subject2 = _appDbContext.Subjects.Skip(1).First();
            var lessonHour1 = _appDbContext.LessonHours.First().Id;
            var lessonHour2 = _appDbContext.LessonHours.Skip(1).First().Id;
            var lessonHour3 = _appDbContext.LessonHours.Skip(2).First().Id;
            Console.WriteLine(lessonHour1);
            var scheduleEntries = new List<ScheduleEntry>()
            {
                new ScheduleEntry() { SubjectId = subject1.Id, GroupId = group.Id, Day = DayOfWeek.Monday, LessonHourId = lessonHour1},
                new ScheduleEntry() { SubjectId = subject1.Id, GroupId = group.Id, Day = DayOfWeek.Monday, LessonHourId = lessonHour2},
                new ScheduleEntry() { SubjectId = subject2.Id, GroupId = group.Id, Day = DayOfWeek.Monday, LessonHourId = lessonHour3},
                new ScheduleEntry() { SubjectId = subject2.Id, GroupId = group1.Id, Day = DayOfWeek.Monday, LessonHourId = lessonHour1},
                new ScheduleEntry() { SubjectId = subject1.Id, GroupId = group1.Id, Day = DayOfWeek.Tuesday, LessonHourId = lessonHour1},
                new ScheduleEntry() { SubjectId = subject2.Id, GroupId = group1.Id, Day = DayOfWeek.Tuesday, LessonHourId = lessonHour2},
            };
            return scheduleEntries;
        }

    }
}
