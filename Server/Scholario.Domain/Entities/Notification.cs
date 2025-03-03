using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Domain.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public virtual Person Receiver { get; set; } = default!;
        public int ReceiverId { get; set; } 
        public string Message { get; set; } = string.Empty; 
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; } = false; 
        public NotificationType Type { get; set; } 
    }
}
