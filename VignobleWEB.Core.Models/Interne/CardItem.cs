using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VignobleWEB.Core.Models.Interne
{
    public class CardItem
    {
        [JsonPropertyName("idProduct")]
        public string IdProduct { get; set; }
        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }

    }
}
