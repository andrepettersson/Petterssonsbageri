namespace bageri.api.ViewModels;

public class SupplierProductViewModel : ProductGetViewModel 
{
    public int SupplierId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public double PricePerKg { get; set; }

}
