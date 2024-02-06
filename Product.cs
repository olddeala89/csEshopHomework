using System.Collections.Generic;
using EShop;

namespace EShop;

public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}