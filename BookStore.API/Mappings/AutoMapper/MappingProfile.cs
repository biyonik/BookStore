using AutoMapper;
using BookStore.API.DataTransferObjects.Book;
using BookStore.API.Enumerations;
using BookStore.API.Models;
using BookStore.API.ViewModels.Books;

namespace BookStore.API.Mappings.AutoMapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<BookForAddDto, Book>().ReverseMap();
            CreateMap<BookForUpdateDto, Book>().ReverseMap();
            CreateMap<Book, BookDetailViewModel>().ForMember(
                destination =>  destination.Genre, 
                options => options.MapFrom(
                    source => ((GenreEnums)source.GenreId).ToString()
                )
            );
            CreateMap<Book, BooksViewModel>().ForMember(
                destination => destination.Genre,
                options => options.MapFrom(
                    source => ((GenreEnums)source.GenreId).ToString()
                )
            );
        }
    }
}