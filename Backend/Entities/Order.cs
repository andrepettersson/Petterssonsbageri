namespace bageri2.api.Entities;

public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; } 
    public string OrderNumber { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}
