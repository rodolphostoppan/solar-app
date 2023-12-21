using SolarApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SolarApp.Infra.EntitiesMap;

public class LocationMap : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.HasKey(prop => prop.Id);
        builder.Property(prop => prop.Id)
            .ValueGeneratedOnAdd();
    }
}

