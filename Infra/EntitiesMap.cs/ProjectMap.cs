using SolarApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SolarApp.Infra.EntitiesMap;

public class ProjectMap : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(prop => prop.Id);
        builder.Property(prop => prop.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(prop => prop.Client);

        builder.HasMany(prop => prop.Bills)
            .WithOne(prop => prop.Project);
    }
}

