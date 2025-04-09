using bageri2.api.ViewModels.OrderItem;

namespace bageri2.api.ViewModels.Order;

public class OrderPutViewModel
{
    public int CustomerId { get; set; }
    public List<OrderItemPutViewModel> OrderItems { get; set; }
}
