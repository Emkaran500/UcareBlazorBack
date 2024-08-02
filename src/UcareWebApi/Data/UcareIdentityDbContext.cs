namespace UcareApp.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UcareApp.Auth.Models;

public class UcareIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public UcareIdentityDbContext(DbContextOptions<UcareIdentityDbContext> options)
        : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(entity => {
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Surname).IsRequired();
            entity.Property(e => e.PhoneNumber).IsRequired();
        });
    }
}