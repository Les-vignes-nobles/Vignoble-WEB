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
        public Guid? SupplierId { get; set; } = new Guid("56d3b267-d07a-421d-a207-70ff1b2b08de");

        [JsonPropertyName("customerId")]
        public Guid CustomerId { get; set; }

        [JsonPropertyName("lineOrders")]
        public List<LineOrderDto> LineOrders { get; set; }
    }
}
