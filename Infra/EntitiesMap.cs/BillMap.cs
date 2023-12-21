using SolarApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SolarApp.Infra.EntitiesMap;

public class BillMap : IEntityTypeConfiguration<Bill>
{
    public void Configure(EntityTypeBuilder<Bill> builder)
    {
        builder.HasKey(prop => prop.Id);
        builder.Property(prop => prop.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(prop => prop.Project)
            .WithMany(prop => prop.Bills);

        builder.HasOne(prop => prop.Location);
    }
}

