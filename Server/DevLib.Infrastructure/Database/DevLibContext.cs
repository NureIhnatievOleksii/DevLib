using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DevLib.Domain.CustomerAggregate;
using DevLib.Domain.UserAggregate;
using DevLib.Domain.PostAggregate;
using DevLib.Domain.TagAggregate;

namespace DevLib.Infrastructure.Database;

public class DevLibContext(DbContextOptions<DevLibContext> options) : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Post> Posts { get; set; } 
    public DbSet<Tag> Tags { get; set; }

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

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(c => c.TagId);

            entity.Property(c => c.TagId)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("NEWID()");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(p => p.PostId);

            entity.Property(p => p.PostId)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("NEWID()");

            entity.Property(p => p.Title)
                  .IsRequired()
                  .HasMaxLength(255);

            entity.Property(p => p.Text)
                  .IsRequired();

            entity.HasOne(p => p.User)
                  .WithMany(u => u.Posts)
                  .HasForeignKey(p => p.UserId)
                  .OnDelete(DeleteBehavior.Cascade);  
        });
    }
}
