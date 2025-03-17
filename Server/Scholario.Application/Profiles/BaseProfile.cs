using AutoMapper;
using Scholario.Application.Dtos;
using Scholario.Application.Dtos.AttendanceRecord;
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
            CreateMap<Student, ReadStudentWithFilteredGradesDto>()
                .ForMember(dest => dest.Grades, opt => opt.MapFrom((src, dest, _, context) =>
                    src.Grades.Where(g => g.SubjectId == (int)context.Items["subjectId"])));


            CreateMap<RegisterUserDto, Person>();
            CreateMap<CreateSubjectDto, Subject>();
            CreateMap<Grade, ReadGradeByStudentDto>()
                .ForMember(desc => desc.SubjectName, x => x.MapFrom(src => src.Subject.Name))
                .ForMember(desc => desc.GradeWeight, x => x.MapFrom(src => src.GradeWeight))
                .ForMember(desc => desc.GradeWeightName, x => x.MapFrom(src => src.GradeWeight.ToString()));
            CreateMap<Subject, ReadSubjectDto>()
                .ForMember(dest => dest.TeacherName, x => x.MapFrom(src => $"{src.Teacher.FirstName} {src.Teacher.LastName}"));
            CreateMap<Student, ParentSubjectDto>()
                .ForMember(dest => dest.ChildId, x => x.MapFrom(src => src.Id))
                .ForMember(dest => dest.ChildName, x => x.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Subjects, x => x.MapFrom(src => src.Group.Subjects));
            CreateMap<Student, ParentGroupDto>()
                .ForMember(dest => dest.ChildId, x => x.MapFrom(src => src.Id))
                .ForMember(dest => dest.ChildName, x => x.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Group, x => x.MapFrom(src => src.Group));
            CreateMap<ScheduleEntry, ScheduleEntryDto>()
                .ForMember(dest => dest.LessonNumber, x => x.MapFrom(src => src.LessonHour.LessonNumber))
                .ForMember(desc => desc.SubjectName, x => x.MapFrom(src => src.Subject.Name));
            CreateMap<ScheduleEntryDto, ScheduleEntry>();
            CreateMap<Group, ReadGroupDto>()
                .ForMember(dest => dest.TeacherName, x => x.MapFrom(src => $"{src.Teacher.FirstName} {src.Teacher.LastName}"))
                .ForMember(dest => dest.Students, x => x.MapFrom(src => src.Students.Select(s => $"{s.FirstName} {s.LastName}")));

            CreateMap<LessonHourDto, LessonHour>();

            CreateMap<AttendanceRecordDto, AttendanceRecord>();

            CreateMap<SubjectGradesDto, Grade>();

        }
    }
}
