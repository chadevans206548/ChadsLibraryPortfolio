namespace ChadsLibraryPortfolio.Model.Entities
{
    public class Book
    {
        public Book()
        {
            BookId = 0;
            Title = string.Empty;
            Author = string.Empty;
            Description = string.Empty;
            CoverImage = string.Empty;
            Publisher = string.Empty;
            PublicationDate = null;
            Category = string.Empty;
            Isbn = string.Empty;
            PageCount = 0;
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
    }
}
