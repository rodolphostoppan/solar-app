using SolarApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SolarApp.Infra.EntitiesMap;

public class ClientMap : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(prop => prop.Id);
        builder.Property(prop => prop.Id)
            .ValueGeneratedOnAdd();
    }
}

