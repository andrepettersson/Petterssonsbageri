namespace bageri2.api.ViewModels.Customer;

public class CustomerPostViewModel : CustomerBaseViewModel
{
    public string ContactPerson { get; set; }
    public string  DeliveryAddress { get; set; }
    public string  InvoiceAddress { get; set; }
}
