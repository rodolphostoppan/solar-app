namespace SolarApp.Entities;

public class Bill
{
    public Guid Id { get; set; }
    public int UC { get; set; }
    public string? Holder { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; }
    public decimal Amount { get; set; }
    public decimal Tariff { get; set; }
    public decimal Consumption { get; set; }
    public Project? Project { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
}

