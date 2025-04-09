using bageri2.api.ViewModels.Order;

namespace bageri2.api.ViewModels.Customer;

public class CustomerGetViewModel : CustomerBaseViewModel
{
    public string ContactPerson { get; set; }
    public string DeliveryAddress { get; set; }
    public string InvoiceAddress { get; set; }

    public IList<OrderGetViewModel> Orders { get; set; }
    
}
