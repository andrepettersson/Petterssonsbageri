using bageri2.api.ViewModels.OrderItem;

namespace bageri2.api.ViewModels.Order;

public class OrderPostViewModel : OrderBaseViewModel
{
    public int CustomerId { get; set; }
    public List<OrderItemPostViewModel> OrderItems { get; set; }
}

