using AutoMapper;
using ChadsLibraryPortfolio.Model.Entities;
using ChadsLibraryPortfolio.ViewModels.Books;

namespace ChadsLibraryPortfolio.AutoMapper.Profiles;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {

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
            .ForMember(dest => dest.AverageUserRating, opt => opt.MapFrom<AverageUserRatingResolver>())
            .ForMember(dest => dest.Available, opt => opt.MapFrom<BookAvailableResolver>())
            ;

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
