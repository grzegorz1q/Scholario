using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Dtos
{
    public class AddMessageOrNoteToStudentDto
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Content {  get; set; } = string.Empty;
        public MessageType MessageType { get; set; }
    }
}
