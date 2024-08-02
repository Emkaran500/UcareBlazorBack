namespace UcareApp.Data;
using Microsoft.EntityFrameworkCore;
using UcareApp.Models;

public class UcarePlacesDbContext : DbContext
{
    public UcarePlacesDbContext(DbContextOptions<UcarePlacesDbContext> options)
        : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Place>(entity => {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired();
            entity.Property(p => p.Adress).IsRequired();
            entity.Property(p => p.ServiceType).IsRequired();
            entity.Property(p => p.WorkingDays).IsRequired();
            entity.Property(p => p.Latitude).IsRequired();
            entity.Property(p => p.Longitude).IsRequired();
            entity.Property(p => p.Rating).HasDefaultValue<double>(0);
        });
    }
}