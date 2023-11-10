namespace VignobleWEB.Core.Models;

public record ApiSettings
{

    public const string Api = "Api";

    public string? BaseUrl { get; set; }
}