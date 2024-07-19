namespace ChadsLibraryPortfolio.Model.Entities;

public class Review
{
    public Review()
    {
        this.ReviewId = 0;
        this.BookId = 0;
        this.Rating = 0;
        this.Description = string.Empty;
    }
    public int ReviewId { get; set; }
    public int BookId { get; set; }
    public Book? Book { get; set; }
    public int Rating { get; set; }
    public string Description { get; set; }
}
