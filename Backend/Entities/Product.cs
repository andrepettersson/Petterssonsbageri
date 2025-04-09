using bageri2.api.Entities;

namespace bageri.api.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ItemNumber { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }
        public int PackSize { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime ManufacturerDate { get; set; }


        public List<SupplierProduct> SupplierProducts {get; set;}
        public List<OrderItem> OrderItems { get; set; }
    }
}