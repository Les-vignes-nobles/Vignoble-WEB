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

        [JsonPropertyName("OrderAuto")]
        public bool OrderAuto { get; set; }

        [JsonPropertyName("SupplierId")]
        public Guid SupplierId { get; set; }
        [JsonPropertyName("Supplier")]
        public Supplier Supplier { get; set; }

        [JsonPropertyName("LineOrders")]
        public ICollection<LineOrder>? LineOrders { get; set; }

        [JsonPropertyName("StockId")]
        public Guid StockId { get; set; }
        [JsonPropertyName("Stock")]
        public Stock Stock { get; set; }

        [JsonPropertyName("pictureId")]
        public string? PictureId { get; set; }
        [JsonPropertyName("picture")]
        public Picture Picture { get; set; }

    }
}
