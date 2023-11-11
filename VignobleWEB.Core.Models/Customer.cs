using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VignobleWEB.Core.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerName { get; set; }
        public string Gender { get; set; }
        public int? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public int? ZipCode { get; set; }
        public string? Town { get; set; }
        public string? Country { get; set; }
        public bool Activated { get; set; }
        public ICollection<HeaderOrder>? HeaderOrders { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
