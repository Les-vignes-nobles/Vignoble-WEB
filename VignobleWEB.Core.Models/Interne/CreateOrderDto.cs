using System.Text.Json.Serialization;

namespace VignobleWEB.Core.Models.Interne
{
    public class CreateOrderDto
    {
        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; } = DateTime.Now;

        [JsonPropertyName("paid")]
        public bool Paid { get; set; }

        [JsonPropertyName("supplierId")]
        public Guid? SupplierId { get; set; } = new Guid("00000000-0000-0000-0000-000000000000");

        [JsonPropertyName("customerId")]
        public Guid CustomerId { get; set; }

        [JsonPropertyName("lineOrders")]
        public List<LineOrderDto> LineOrders { get; set; }
    }
}
