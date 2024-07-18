using ChadsLibraryPortfolio.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChadsLibraryPortfolio.Models;
public class LibraryContext :DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<InventoryLog> InventoryLogs { get; set; }
}
