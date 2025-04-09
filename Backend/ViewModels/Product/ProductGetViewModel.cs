using bageri2.api.ViewModels.Product;

namespace bageri.api.ViewModels;

public class ProductGetViewModel 
{
    public int ProductId { get; set; }
    public string ItemNumber { get; set; }
    public int PackSize  { get; set; }
    public DateTime ExpiryDate { get; set; }
    public DateTime ManufacturerDate { get; set; }
}
