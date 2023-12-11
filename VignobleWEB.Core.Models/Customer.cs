using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Le nom est requis")]
        public string CustomerSurname { get; set; }

        [JsonProperty("customerName")]
        [Required(ErrorMessage = "Le prénom est requis")]
        public string CustomerName { get; set; }

        [JsonProperty("gender")]
        [Required(ErrorMessage = "Le genre est requis")]
        public string Gender { get; set; }

        [JsonProperty("phoneNumber")]
        public string? PhoneNumber { get; set; }

        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("address")]
        [Required(ErrorMessage = "L'adresse est requise")]
        public string? Address { get; set; }

        [JsonProperty("zipCode")]
        [Required(ErrorMessage = "Le code postal est requis")]
        public int? ZipCode { get; set; }

        [JsonProperty("town")]
        [Required(ErrorMessage = "La ville est requise")]
        public string? Town { get; set; }

        [JsonProperty("country")]
        [Required(ErrorMessage = "Le pays est requis")]
        public string? Country { get; set; }

        public bool Activated { get; set; }

        public ICollection<HeaderOrder>? HeaderOrders { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }
        //public User? User { get; set; }
    }
}
