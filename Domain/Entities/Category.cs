using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Categories")]
public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<Product>? Products { get; set; }
}
