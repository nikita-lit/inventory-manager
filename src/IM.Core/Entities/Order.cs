namespace IM.Core.Entities;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public ICollection<OrderLine> Lines { get; set; } = new List<OrderLine>();
}
