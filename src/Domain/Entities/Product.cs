namespace CleanArchitecture.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Image { get; set; } = "";
    public double Price { get; set;}    
}
