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
        public PrepDatabase(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Seed()
        {
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
        }

        private IEnumerable<Teacher> GetTeachers()
        {
            var teachers = new List<Teacher>()
            {
                new Teacher(){FirstName="Jan", LastName="Kowlaski", Email="jan.kowalski@tests.pl", Password="jankowalski"},
                new Teacher(){FirstName="Anna", LastName="Maliszewska", Email="anna.maliszewska@tests.pl", Password="annamaliszewska", GroupId=1},
            };
            return teachers;
        }
        private IEnumerable<Parent> GetParents()
        {
            var parents = new List<Parent>()
            {
                new Parent(){FirstName="Edyta", LastName="Bąk", Email="edyta.bak@test.pl", Password="edytabak", PhoneNumber="987654321", Address="Topolowa 1"},
                new Parent(){FirstName="Karol", LastName="Kot", Email="karol.kot@test.pl", Password="karolkot", PhoneNumber="123456789", Address="Akacjowa 1"}
            };
            return parents;
        }
        private IEnumerable<Student> GetStudents()
        {
            var parentId = _appDbContext.Persons.OfType<Parent>().First().Id;
            var students = new List<Student>()
            {
                new Student(){FirstName="Adam", LastName="Nowak", Email="adam.nowak@test.pl", Password="adamnowak", GroupId=1, ParentId=parentId}
            };
            return students;
        }
        private IEnumerable<Group> GetGroups()
        {
            var groups = new List<Group>()
            {
                new Group() {Name = "1tb1"}
            };
            return groups;
        }
        private IEnumerable<Subject> GetSubjects()
        {
            var teacherId = _appDbContext.Persons.OfType<Teacher>().First().Id;
            var subjects = new List<Subject>()
            {
                new Subject(){ Name="Matematyka", TeacherId=teacherId, Description="Przedmiot pozwalający zrozumieć logiczne myślenie"}
            };
            return subjects;
        }
    }
}
