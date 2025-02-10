using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Domain.Entities
{
    public abstract class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Role Role { get; set; }
        public virtual ICollection<Message> SentMessages { get; set; } = default!;
        public virtual ICollection<Message> ReceivedMessages { get; set; } = default!;
        public virtual ICollection<Notification> Notifications { get; set; } = default!;
    }
}
