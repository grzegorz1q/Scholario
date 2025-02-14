using AutoMapper;
using Scholario.Application.Dtos;
using Scholario.Application.Dtos.Grade;
using Scholario.Application.Dtos.Message;
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
            CreateMap<ReadStudentDto,Student>();
            CreateMap<Student, ReadStudentDto>();
            CreateMap<RegisterUserDto, Person>();
            CreateMap<CreateSubjectDto, Subject>();
            CreateMap<Grade, ReadGradeByStudentDto>();
        }
    }
}
