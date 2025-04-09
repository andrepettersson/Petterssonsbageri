namespace bageri.api.Entities;

    public class SupplierProduct
    {
        public int SupplierId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ItemNumber { get; set; }
        public int Quantity { get; set; }
        public double PricePerKg { get; set; }

        public Product Product {get;set;}
        public Supplier Supplier { get; set; }
        
    }
