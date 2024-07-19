namespace ChadsLibraryPortfolio.ViewModels.Reviews;
public class AddReviewViewModel
{
    public AddReviewViewModel()
    {
        this.BookId = 0;
        this.Rating = 0;
        this.Description = string.Empty;
    }
    public int BookId { get; set; }
    public int Rating { get; set; }
    public string Description { get; set; }
}
