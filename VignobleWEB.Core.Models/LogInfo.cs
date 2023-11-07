
using Newtonsoft.Json;

namespace VignobleWEB.Core.Models;

public record LogInfo
{
    [JsonProperty("username")]
    public string Email { get; init; }
    [JsonProperty("password")]
    public string Password { get; init; }
}