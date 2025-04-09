using bageri2.api.ViewModels.Customer;
using bageri2.api.ViewModels.OrderItem;

namespace bageri2.api.ViewModels.Order;

public class OrderGetViewModel : OrderBaseViewModel
{
    public int CustomerId { get; set; }
    public CustomerGetViewModel Customer { get; set; }
    public List<OrderItemGetViewModel> OrderItems { get; set; }
}
