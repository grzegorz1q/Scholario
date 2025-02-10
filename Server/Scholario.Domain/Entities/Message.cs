using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Domain.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public virtual Person Sender { get; set; } = default!;
        public int SenderId { get; set; }
        public virtual Person Receiver { get; set; } = default!;
        public int ReceiverId { get; set; } 
        public string Content { get; set; } = string.Empty;
        public DateTime Sent { get; set; } 
        public bool IsRead { get; set; } = false; 
    }
}
