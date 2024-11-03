using AutoMapper;
using DevLib.Application.CQRS.Commands.Customers.CreateCustomer;
using DevLib.Application.CQRS.Commands.Customers.UpdateCustomer;
using DevLib.Application.CQRS.Commands.Notes.AddNote;
using DevLib.Application.CQRS.Commands.Bookmarks.AddBookmark;
using DevLib.Application.CQRS.Commands.Directories.CreateDirectories;
using DevLib.Application.CQRS.Commands.Directories.UpdateDirectories;
using DevLib.Application.CQRS.Commands.Books.UpdateBook;
using DevLib.Application.CQRS.Commands.Books.CreateBooks;
using DevLib.Application.CQRS.Dtos.Queries;
using DevLib.Domain.CustomerAggregate;
using DevLib.Domain.DirectoryAggregate;
using DevLib.Domain.ArticleAggregate;
using DevLib.Domain.BookmarkAggregate;
using DevLib.Domain.NotesAggregate;
using DevLib.Domain.BookAggregate;
using DevLib.Domain.TagAggregate;
using DevLib.Domain.CommentAggregate;
using DevLib.Application.CQRS.Queries.Books.SearchBooks;
using DevLib.Application.CQRS.Commands.Comments.AddReview;

namespace DevLib.Infrastructure;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddNoteCommand, Note>();
        CreateMap<AddBookmarkCommand, Bookmark>();

        CreateMap<AddReviewCommand, Comment>();

        CreateMap<CreateCustomerCommand, Customer>();
        CreateMap<UpdateCustomerCommand, Customer>();

        CreateMap<Customer, GetCustomerByIdQueryDto>();
        CreateMap<Customer, GetAllCustomersQueryDto>();

        CreateMap<CreateDirectoryCommand, DLDirectory>()
            .ForMember(dest => dest.DirectoryId, opt => opt.MapFrom(src => Guid.NewGuid()));

        CreateMap<UpdateDirectoryCommand, DLDirectory>()
            .ForMember(dest => dest.DirectoryId, opt => opt.MapFrom(src => src.DirectoryId));

        CreateMap<CreateBookCommand, Book>();
        CreateMap<UpdateBookCommand, Book>();

        CreateMap<DLDirectory, DirectoryDto>()
            .ForMember(dest => dest.DirectoryImg, opt => opt.MapFrom(src => src.ImgLink));

        CreateMap<Book, GetBookByIdQueryDto>()
            .ForMember(dest => dest.PDF, opt => opt.MapFrom(src => src.FilePath));
        CreateMap<Book, BookNameDto>();
        CreateMap<Book, LastPublishedBookDto>()
            .ForMember(dest => dest.PhotoBook, opt => opt.MapFrom(src => src.BookImg));

        CreateMap<Article, GetArticleByIdQueryDto>();
        CreateMap<Article, GetAllArticlesNamesByDirectoryIdDto>();

        CreateMap<Tag, TagDto>();

        CreateMap<DLDirectory, LastDirectoryDto>()
            .ForMember(dest => dest.DirectoryId, opt => opt.MapFrom(src => src.DirectoryId))
            .ForMember(dest => dest.DirectoryName, opt => opt.MapFrom(src => src.DirectoryName))
            .ForMember(dest => dest.ImgLink, opt => opt.MapFrom(src => src.ImgLink));
    }
}
