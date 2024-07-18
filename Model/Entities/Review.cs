namespace ChadsLibraryPortfolio.Model.Entities
{
    public class Review
    {
        public Review()
        {
            ReviewId = 0;
            BookId = 0;
            Rating = 0;
            Description = string.Empty;
        }
        public int ReviewId { get; set; }
        public int BookId { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
    }
}
