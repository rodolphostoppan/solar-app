namespace SolarApp.Entities;

public class Project
{
    public Guid Id { get; set; }
    public Client? Client { get; set; }
    public ICollection<Bill>? Bills { get; set; }
    public Location? Location { get; set; }
    public bool Ground { get; set; } = false;
    public decimal Power { get; set; }
    public decimal Modules { get; set; }
    public decimal ModulesPower { get; set; }
    public int Inverter { get; set; }
    public decimal Generation { get; set; }
    public MonthGeneration? MonthGeneration { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }

    // public decimal Area { get; set; } adicionar se der tempo
}
