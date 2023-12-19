namespace SolarApp.Entities;

public class Client
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
    // public Seller Seller { get; set; } no futuro pode adicionar
}

