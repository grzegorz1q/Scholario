using AutoMapper;
using Scholario.Application.Dtos;
using Scholario.Application.Dtos.Grade;
using Scholario.Application.Dtos.Group;
using Scholario.Application.Dtos.Message;
using Scholario.Application.Dtos.Parent;
using Scholario.Application.Dtos.ScheduleEntries;
using Scholario.Application.Dtos.Student;
using Scholario.Application.Dtos.Subject;
using Scholario.Application.Dtos.Teacher;
using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Profiles
{
    public class BaseProfile : Profile
    {
        public BaseProfile()
        {
            CreateMap<AddOrUpdateGradeToStudentDto, Grade>();
            CreateMap<AddMessageOrNoteToStudentDto, Message>();
            CreateMap<ReadStudentDto, Student>();
            CreateMap<Student, ReadStudentDto>();
            CreateMap<RegisterUserDto, Person>();
            CreateMap<CreateSubjectDto, Subject>();
            CreateMap<Grade, ReadGradeByStudentDto>();
            CreateMap<Group, ReadGroupDto>()
                .ForMember(dest => dest.TeacherName, x => x.MapFrom(src => $"{src.Teacher.FirstName} {src.Teacher.LastName}"));
            CreateMap<Subject, ReadSubjectDto>()
                .ForMember(dest => dest.TeacherName, x => x.MapFrom(src => $"{src.Teacher.FirstName} {src.Teacher.LastName}"));
            CreateMap<Student, ParentSubjectDto>()
                .ForMember(dest => dest.ChildId, x => x.MapFrom(src => src.Id))
                .ForMember(dest => dest.ChildName, x => x.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Subjects, x => x.MapFrom(src => src.Group.Subjects));

            CreateMap<ScheduleEntry, ScheduleEntryDto>();
            CreateMap<ScheduleEntryDto, ScheduleEntry>();
            CreateMap<Group, ReadGroupDto>()
                .ForMember(dest => dest.TeacherName, x => x.MapFrom(src => $"{src.Teacher.FirstName} {src.Teacher.LastName}"));
        }
    }
}
