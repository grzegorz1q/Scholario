﻿using Scholario.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Interfaces
{
    public interface ITeacherService
    {
        Task<ReadStudentDto?> GetStudentById(ReadStudentDto readStudentDto);
        Task AddMessageOrNoteToStudent(AddMessageOrNoteToStudentDto addNoteToStudentDto);
    }
}
