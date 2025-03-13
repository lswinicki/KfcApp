using Domain.Entities;

namespace Domain.Models;

public class Cart
{
    public List<CartItem> Products { get; set; } = new List<CartItem>();
    public int UserId { get; set; }
    public decimal TotalPrice { get; set; }
}

public class CartItem
{
    public Product Product { get; set; }
    public int Quantity { get; set; }
}