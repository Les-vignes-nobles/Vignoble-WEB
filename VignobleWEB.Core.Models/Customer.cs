using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VignobleWEB.Core.Models
{
    public class Customer
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("customerSurname")]
        public string CustomerSurname { get; set; }

        [JsonProperty("customerName")]
        public string CustomerName { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("phoneNumber")]
        public string? PhoneNumber { get; set; }

        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("address")]
        public string? Address { get; set; }

        [JsonProperty("zipCode")]
        public int? ZipCode { get; set; }

        [JsonProperty("town")]
        public string? Town { get; set; }

        [JsonProperty("country")]
        public string? Country { get; set; }

        public bool Activated { get; set; }

        public ICollection<HeaderOrder>? HeaderOrders { get; set; }

        public string UserId { get; set; }
        public User? User { get; set; }
    }
}
