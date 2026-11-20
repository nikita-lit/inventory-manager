namespace IM.Core.Entities;

public class OrderLine
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    // Capture the category and price at the time of sale for historical reports.
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public StockMovement? StockMovement { get; set; }
}
