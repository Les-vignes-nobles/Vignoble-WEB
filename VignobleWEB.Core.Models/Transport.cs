using Newtonsoft.Json;

namespace VignobleWEB.Core.Models;

public class Transport
{
    [JsonProperty("id")]
    public Guid Id { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("delayDelivery")]
    public int DelayDelivery { get; set; }
    [JsonProperty("price")]
    public double Price { get; set; }
}