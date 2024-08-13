using ChadsLibraryPortfolio.Model.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChadsLibraryPortfolio.Models;

public class LibraryContext(DbContextOptions<LibraryContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<InventoryLog> InventoryLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new RoleConfiguration());

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(prop => prop.BookId);

            entity.Property(prop => prop.Title).IsRequired().HasColumnType("NVARCHAR(255)");
            entity.Property(prop => prop.Author).IsRequired().HasColumnType("NVARCHAR(255)");
            entity.Property(prop => prop.Description).IsRequired().HasColumnType("NVARCHAR(255)");
            entity.Property(prop => prop.CoverImage).IsRequired().HasColumnType("NVARCHAR(255)");
            entity.Property(prop => prop.Publisher).IsRequired().HasColumnType("NVARCHAR(255)");
            entity.Property(prop => prop.PublicationDate).HasColumnType("DATE");
            entity.Property(prop => prop.Category).IsRequired().HasColumnType("NVARCHAR(255)");
            entity.Property(prop => prop.Isbn).IsRequired().HasColumnType("NVARCHAR(255)");
            entity.Property(prop => prop.PageCount).IsRequired();

            entity.HasIndex(index => index.Isbn).IsUnique().HasDatabaseName("IX_Book_Isbn");
            entity.HasIndex(index => new { index.Title, index.Author }).IsUnique().HasDatabaseName("IX_Book_TitleAuthor");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(prop => prop.ReviewId);

            entity.Property(prop => prop.BookId).IsRequired();
            entity.Property(prop => prop.Rating).IsRequired();
            entity.Property(prop => prop.Description).IsRequired().HasColumnType("NVARCHAR(255)");
        });

        modelBuilder.Entity<InventoryLog>(entity =>
        {
            entity.HasKey(prop => prop.InventoryLogId);

            entity.Property(prop => prop.BookId).IsRequired();
            entity.Property(prop => prop.User).IsRequired().HasColumnType("NVARCHAR(255)");
            entity.Property(prop => prop.CheckoutDate).HasColumnType("DATE");
            entity.Property(prop => prop.CheckinDate).HasColumnType("DATE");
            entity.Property(prop => prop.DueDate).HasColumnType("DATE");
        });
    }
}

public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
        new IdentityRole
        {
            Name = "Librarian",
            NormalizedName = "LIBRARIAN"
        },
        new IdentityRole
        {
            Name = "Customer",
            NormalizedName = "CUSTOMER"
        });
    }
}
