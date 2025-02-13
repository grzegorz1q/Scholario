using Scholario.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Interfaces
{
    public interface IGradeService
    {
        Task AddGradeToStudent(AddOrUpdateGradeToStudentDto addGradeToStudentDto, int teacherId);
        Task UpdateStudentGrade(AddOrUpdateGradeToStudentDto updateStudentGradeDto, int teacherId);
        Task DeleteGradeFromStudent(int id);
    }
}
