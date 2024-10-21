using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DevLib.Domain.CustomerAggregate;
using DevLib.Domain.UserAggregate;
using DevLib.Domain.PostAggregate;
using DevLib.Domain.TagAggregate;
using DevLib.Domain.DirectoryAggregate;
using DevLib.Domain.DirectoryLinkAggregate;
using DevLib.Domain.BookAggregate;
using DevLib.Domain.BookmarkAggregate;

namespace DevLib.Infrastructure.Database;

public class DevLibContext(DbContextOptions<DevLibContext> options) : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Post> Posts { get; set; } 
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<DLDirectory> Directories { get; set; }
    public DbSet<DirectoryLink> DirectoryLinks { get; set; }
    public DbSet<Bookmark> Bookmarks { get; set; }

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

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(c => c.BookId);

            entity.Property(c => c.BookId)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("NEWID()");
        });

        // Настройка Bookmark
        modelBuilder.Entity<Bookmark>(entity =>
        {
            entity.HasKey(b => b.BookmarkId);

            entity.HasOne(b => b.Book)
                  .WithMany()
                  .HasForeignKey(b => b.BookId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(b => b.User)
                  .WithMany()
                  .HasForeignKey(b => b.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
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

        modelBuilder.Entity<DLDirectory>(entity =>
        {
            entity.HasKey(d => d.DirectoryId);

            entity.Property(d => d.DirectoryId)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("NEWID()");
        });

        modelBuilder.Entity<DirectoryLink>(entity =>
        {
            entity.HasKey(dl => dl.DirectoryLinkId);

            entity.Property(dl => dl.DirectoryLinkId)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("NEWID()");

            entity.Property(dl => dl.ChapterName)
                  .IsRequired()
                  .HasMaxLength(255);

            entity.HasOne(dl => dl.Directory)
                  .WithMany(d => d.DirectoryLinks)
                  .HasForeignKey(dl => dl.DirectoryId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(dl => dl.Post)
                  .WithMany(p => p.DirectoryLinks) 
                  .HasForeignKey(dl => dl.ArticleId)
                  .OnDelete(DeleteBehavior.Cascade);  
        });
    }
}
