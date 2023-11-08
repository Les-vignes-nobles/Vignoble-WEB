namespace VignobleWEB.Core.Models;

public class Transport
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTimeOffset DateTimeOffset { get; set; }
    public double Price { get; set; }
}