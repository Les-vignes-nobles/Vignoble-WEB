using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VignobleWEB.Core.Models.Interne
{
    public class LineOrderDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("quantitySupplied")]
        public int QuantitySupplied { get; set; }

        [JsonPropertyName("headerOrderId")]
        public Guid HeaderOrderId { get; set; }

        [JsonPropertyName("productId")]
        public Guid ProductId { get; set; }
    }
}
