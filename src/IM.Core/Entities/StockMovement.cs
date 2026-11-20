namespace IM.Core.Entities;

public enum StockMovementDirection
{
    Incoming = 1,
    Outgoing = 2
}

public class StockMovement
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public StockMovementDirection Direction { get; set; }
    public int Quantity { get; set; }
    public DateTimeOffset OccurredAt { get; set; } = DateTimeOffset.UtcNow;
    public string? Reason { get; set; }
    public Guid? OrderLineId { get; set; }
    public OrderLine? OrderLine { get; set; }
}
