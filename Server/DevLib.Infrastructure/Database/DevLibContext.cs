using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DevLib.Domain.CustomerAggregate;
using DevLib.Domain.UserAggregate;
using DevLib.Domain.PostAggregate;
using DevLib.Domain.TagAggregate;
using DevLib.Domain.DirectoryAggregate;
using DevLib.Domain.BookAggregate;
using DevLib.Domain.BookmarkAggregate;
using DevLib.Domain.RatingAggregate;
using DevLib.Domain.CommentAggregate;
using DevLib.Domain.ReplyLinkAggregate;
using DevLib.Domain.ArticleAggregate;
using DevLib.Domain.NotesAggregate;

namespace DevLib.Infrastructure.Database;

public class DevLibContext(DbContextOptions<DevLibContext> options) : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Post> Posts { get; set; } 
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<DLDirectory> Directories { get; set; }
    public DbSet<Bookmark> Bookmarks { get; set; }
    public DbSet<TagConnection> TagConnections { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Comment> ReplyLinks { get; set; }
    public DbSet<Article> Articles { get; set; }

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

        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(b => b.ArticleId);

            entity.Property(b => b.ArticleId)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("NEWID()");

            entity.HasOne(b => b.Directory)
                  .WithMany()
                  .HasForeignKey(b => b.DirectoryId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Bookmark>(entity =>
        {
            entity.HasKey(b => b.BookmarkId);

            entity.Property(b => b.BookmarkId)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("NEWID()");

            entity.HasOne(b => b.Book)
                  .WithMany()
                  .HasForeignKey(b => b.BookId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(b => b.User)
                  .WithMany()
                  .HasForeignKey(b => b.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TagConnection>(entity =>
        {
            entity.HasKey(tc => tc.TagConnectionId);

            entity.HasOne(tc => tc.Tag)
                  .WithMany()
                  .HasForeignKey(tc => tc.TagId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(tc => tc.Post)
                  .WithMany()
                  .HasForeignKey(tc => tc.PostId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(tc => tc.Book)
                  .WithMany()
                  .HasForeignKey(tc => tc.BookId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(tc => tc.Directory)
                  .WithMany()
                  .HasForeignKey(tc => tc.DirectoryId)
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

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(r => r.RatingId);

            entity.Property(r => r.RatingId)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("NEWID()");

            entity.HasOne(r => r.Book)
                  .WithMany()
                  .HasForeignKey(r => r.BookId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(r => r.User)
                  .WithMany()
                  .HasForeignKey(r => r.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Property(r => r.PointsQuantity).IsRequired();
        });

        // todo ok come up with a way to delete comments with correct interaction of the Reply_Link table.
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(com => com.CommentId);

            entity.Property(com => com.Content).IsRequired();

            entity.Property(com => com.CommentId)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("NEWID()");

            entity.HasOne(com => com.Book)
                  .WithMany()
                  .HasForeignKey(com => com.BookId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(com => com.Post)
                  .WithMany()
                  .HasForeignKey(com => com.PostId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(com => com.User)
                  .WithMany()
                  .HasForeignKey(com => com.UserId);

            entity.HasOne(com => com.Reply)
                  .WithMany()
                  .HasForeignKey(com => com.ReplyId)
                  .OnDelete(DeleteBehavior.ClientSetNull);

        });

        modelBuilder.Entity<ReplyLink>(entity =>
        {
            entity.HasKey(r => r.ReplyId);

            entity.Property(r => r.ReplyId)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("NEWID()");

            entity.HasOne(r => r.Comment)
                  .WithMany()
                  .HasForeignKey(r => r.CommentId)
                  .OnDelete(DeleteBehavior.Cascade);

        });

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(n => n.NoteId);

            entity.Property(n => n.NoteId)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("NEWID()");

            entity.HasOne(n => n.Book)
                  .WithMany()
                  .HasForeignKey(n => n.BookId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(n => n.User)
                  .WithMany()
                  .HasForeignKey(n => n.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

        });


    }
}
