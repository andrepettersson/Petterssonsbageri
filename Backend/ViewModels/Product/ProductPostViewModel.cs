using bageri2.api.ViewModels.Product;

namespace bageri.api.ViewModels;

public class ProductPostViewModel : ProductBaseViewModel
{
   public string ItemNumber { get; set; }
   public double Weight { get; set; }
   public int PackSize { get; set; }
   public DateTime ExpiryDate { get; set; }
   public DateTime ManufacturerDate { get; set; }

}
