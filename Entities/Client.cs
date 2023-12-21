namespace SolarApp.Entities;

public class Client
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Location? Location { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
    // public Seller Seller { get; set; } no futuro pode adicionar
}

