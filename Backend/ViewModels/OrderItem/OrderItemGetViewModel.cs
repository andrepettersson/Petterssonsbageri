namespace bageri2.api.ViewModels.OrderItem;

public class OrderItemGetViewModel : OrderItemBaseViewModel
{
    public string ProductName { get; set; }
    public double TotalPrice => Quantity * Price;
}
