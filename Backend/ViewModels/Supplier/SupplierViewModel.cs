namespace bageri.api.ViewModels;

public class SupplierViewModel
{
    public int SupplierId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ContactPerson { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public IList<ProductGetViewModel> Products {get; set;}
}
