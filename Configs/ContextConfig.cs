using Microsoft.EntityFrameworkCore;
using SolarApp.Entities;
using SolarApp.Infra.EntitiesMap;

namespace SolarApp.Configs;

public class ContextConfig : DbContext
{
    public required DbSet<Client> Client { get; set; }
    public required DbSet<Bill> Bill { get; set; }
    public required DbSet<Project> Project { get; set; }

    public ContextConfig(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ClientMap());
        modelBuilder.ApplyConfiguration(new BillMap());
        modelBuilder.ApplyConfiguration(new ProjectMap());
    }
}

