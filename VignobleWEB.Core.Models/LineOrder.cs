namespace VignobleWEB.Core.Models;

public class LineOrder
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public int QuantitySupplied { get; set; }
    public Guid HeaderOrderId { get; set; }
    public HeaderOrder HeaderOrder { get; set; }
    public Product Product { get; set; }
}