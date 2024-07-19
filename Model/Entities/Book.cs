namespace ChadsLibraryPortfolio.Model.Entities;

public class Book
{
    public Book()
    {
        this.BookId = 0;
        this.Title = string.Empty;
        this.Author = string.Empty;
        this.Description = string.Empty;
        this.CoverImage = string.Empty;
        this.Publisher = string.Empty;
        this.PublicationDate = null;
        this.Category = string.Empty;
        this.Isbn = string.Empty;
        this.PageCount = 0;
    }
    public int BookId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public string CoverImage { get; set; }
    public string Publisher { get; set; }
    public DateOnly? PublicationDate { get; set; }
    public string Category { get; set; }
    public string Isbn { get; set; }
    public int PageCount { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = [];
    public virtual ICollection<InventoryLog> InventoryLogs { get; set; } = [];
}
