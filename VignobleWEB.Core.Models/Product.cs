using System.Text.Json.Serialization;

namespace VignobleWEB.Core.Models
{
    public class Product
    {
        [JsonPropertyName("Id")]
        public Guid Id { get; set; }
        [JsonPropertyName("Reference")]
        public string Reference { get; set; }
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [JsonPropertyName("Description")]
        public string Description { get; set; }
        [JsonPropertyName("Year")]
        public int Year { get; set; }
        [JsonPropertyName("UnitPrice")]
        public double UnitPrice { get; set; }
        [JsonPropertyName("Activated")]
        public bool Activated { get; set; }
        [JsonPropertyName("Category")]
        public int Category { get; set; }
        [JsonPropertyName("Image")]
        public string? Image { get; set; }
        [JsonPropertyName("OrderAuto")]
        public bool OrderAuto { get; set; }

        [JsonPropertyName("SupplierId")]
        public int SupplierId { get; set; }
        [JsonPropertyName("Supplier")]
        public Supplier Supplier { get; set; }

        [JsonPropertyName("LineOrders")]
        public ICollection<LineOrder>? LineOrders { get; set; }

        [JsonPropertyName("StockId")]
        public int StockId { get; set; }
        [JsonPropertyName("Stock")]
        public Stock Stock { get; set; }

    }
}
