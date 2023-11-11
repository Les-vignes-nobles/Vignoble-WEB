using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VignobleWEB.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public int Role { get; set; }
        public string Username { get; set; }
        public DateTime BirthDay { get; set; }
        public String EncryptPassword { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool Activated { get; set; }
    }
}
