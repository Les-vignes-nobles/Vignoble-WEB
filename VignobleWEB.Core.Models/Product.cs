using System.Text.Json.Serialization;

namespace VignobleWEB.Core.Models
{
    public class Product
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("Reference")]
        public string Reference { get; set; }
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [JsonPropertyName("Description")]
        public string Description { get; set; }
        [JsonPropertyName("Year")]
        public int Year { get; set; }
        [JsonPropertyName("unitPrice")]
        public double UnitPrice { get; set; }
        [JsonPropertyName("activated")]
        public bool Activated { get; set; }
        [JsonPropertyName("category")]
        public int Category { get; set; }

        [JsonPropertyName("OrderAuto")]
        public bool OrderAuto { get; set; }

        [JsonPropertyName("SupplierId")]
        public Guid SupplierId { get; set; }
        [JsonPropertyName("Supplier")]
        public Supplier Supplier { get; set; }

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
