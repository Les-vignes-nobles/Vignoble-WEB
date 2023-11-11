using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VignobleWEB.Core.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public int Role { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
        public String EncryptPassword { get; set; }
        public bool Activated { get; set; }
    }
}
