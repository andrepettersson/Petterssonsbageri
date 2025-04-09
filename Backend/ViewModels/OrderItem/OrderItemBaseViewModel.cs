namespace bageri2.api.ViewModels;

public class OrderItemBaseViewModel
{
    public int OrderItemId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
}
