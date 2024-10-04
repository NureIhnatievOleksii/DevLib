using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DevLib.Domain.CustomerAggregate;
using DevLib.Domain.UserAggregate;

namespace DevLib.Infrastructure.Database;

public class DevLibContext(DbContextOptions<DevLibContext> options) : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
{  
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Id)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("NEWID()");
        });
    }
}
