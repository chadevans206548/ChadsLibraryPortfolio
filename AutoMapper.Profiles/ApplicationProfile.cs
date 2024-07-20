using AutoMapper;
using ChadsLibraryPortfolio.Model.Entities;
using ChadsLibraryPortfolio.ViewModels.Books;
using ChadsLibraryPortfolio.ViewModels.InventoryLogs;
using ChadsLibraryPortfolio.ViewModels.Reviews;

namespace ChadsLibraryPortfolio.AutoMapper.Profiles;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        #region Review
        this.CreateMap<Review, ReviewViewModel>()
            .ForMember(dest => dest.ReviewId, opt => opt.MapFrom(src => src.ReviewId))
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            ;

        this.CreateMap<AddReviewViewModel, Review>()
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            ;

        #endregion

        #region InventoryLog
        this.CreateMap<InventoryLog, InventoryLogViewModel>()
            .ForMember(dest => dest.InventoryLogId, opt => opt.MapFrom(src => src.InventoryLogId))
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.CheckoutDate, opt => opt.MapFrom(src => src.CheckoutDate))
            .ForMember(dest => dest.CheckinDate, opt => opt.MapFrom(src => src.CheckinDate))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
            ;

        this.CreateMap<AddInventoryLogViewModel, InventoryLog>()
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.CheckoutDate, opt => opt.MapFrom(src => src.CheckoutDate))
            .ForMember(dest => dest.CheckinDate, opt => opt.MapFrom(src => src.CheckinDate))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
            ;

        this.CreateMap<EditInventoryLogViewModel, InventoryLog>()
            .ForMember(dest => dest.InventoryLogId, opt => opt.Ignore())
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.CheckoutDate, opt => opt.MapFrom(src => src.CheckoutDate))
            .ForMember(dest => dest.CheckinDate, opt => opt.MapFrom(src => src.CheckinDate))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
            ;
        #endregion

        #region Book
        this.CreateMap<Book, BookViewModel>()
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CoverImage, opt => opt.MapFrom(src => src.CoverImage))
            .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher))
            .ForMember(dest => dest.PublicationDate, opt => opt.MapFrom(src => src.PublicationDate))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.Isbn, opt => opt.MapFrom(src => src.Isbn))
            .ForMember(dest => dest.PageCount, opt => opt.MapFrom(src => src.PageCount))
            //.ForMember(dest => dest.AverageUserRating, opt => opt.Ignore())
            //.ForMember(dest => dest.Available, opt => opt.Ignore())
            .ForMember(dest => dest.AverageUserRating, opt => opt.MapFrom<AverageUserRatingResolver>())
            .ForMember(dest => dest.Available, opt => opt.MapFrom<BookAvailableResolver>())
            ;

        this.CreateMap<AddBookViewModel, Book>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CoverImage, opt => opt.MapFrom(src => src.CoverImage))
            .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher))
            .ForMember(dest => dest.PublicationDate, opt => opt.MapFrom(src => src.PublicationDate))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.Isbn, opt => opt.MapFrom(src => src.Isbn))
            .ForMember(dest => dest.PageCount, opt => opt.MapFrom(src => src.PageCount))
            ;

        this.CreateMap<EditBookViewModel, Book>()
            .ForMember(dest => dest.BookId, opt => opt.Ignore())
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CoverImage, opt => opt.MapFrom(src => src.CoverImage))
            .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher))
            .ForMember(dest => dest.PublicationDate, opt => opt.MapFrom(src => src.PublicationDate))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.Isbn, opt => opt.MapFrom(src => src.Isbn))
            .ForMember(dest => dest.PageCount, opt => opt.MapFrom(src => src.PageCount))
            ;
        #endregion
    }

    public class AverageUserRatingResolver : IValueResolver<Book, BookViewModel, decimal>
    {
        public decimal Resolve(Book source, BookViewModel dest, decimal destMember, ResolutionContext context)
        {
            if (source.Reviews.Count > 0)
            {
                return source.Reviews.Sum(x => x.Rating) / source.Reviews.Count;
            }
            else
            {
                return 0;
            }
        }
    }

    public class BookAvailableResolver : IValueResolver<Book, BookViewModel, bool>
    {
        public bool Resolve(Book source, BookViewModel dest, bool destMember, ResolutionContext context)
        {
            var log = source.InventoryLogs.OrderByDescending(x => x.CheckoutDate).FirstOrDefault();
            if (log != null)
            {
                return log.CheckinDate.HasValue;
            }
            else
            {
                return true;
            }
        }
    }
}
