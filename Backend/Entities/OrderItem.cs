using bageri.api.Entities;

namespace bageri2.api.Entities;

public class OrderItem
{
    public int OrderItemId { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; } 
    public double TotalPrice => Quantity * Price;

    public Order Order { get; set; }
    public Product Product { get; set; }
}
