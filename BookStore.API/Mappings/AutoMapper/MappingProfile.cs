using AutoMapper;
using BookStore.API.DataTransferObjects.Book;
using BookStore.API.DataTransferObjects.Genre;
using BookStore.API.DataTransferObjects.User;
using BookStore.API.Models;
using BookStore.API.ViewModels.Books;
using BookStore.API.ViewModels.Genres;

namespace BookStore.API.Mappings.AutoMapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // Book Profile
            CreateMap<BookForAddDto, Book>().ReverseMap();
            CreateMap<BookForUpdateDto, Book>().ReverseMap();
            // CreateMap<Book, BookDetailViewModel>();
            // CreateMap<Book, BooksViewModel>();

            CreateMap<Book, BookDetailViewModel>().ForMember(
                destination =>  destination.Genre, 
                options => options.MapFrom(
                    source => source.Genre.Name
                )
            );
            CreateMap<Book, BooksViewModel>().ForMember(
                destination => destination.Genre,
                options => options.MapFrom(
                    source => source.Genre.Name
                )
            );

            // Genre Profile
            CreateMap<Genre, GenreViewModel>().ReverseMap();
            CreateMap<Genre, GenreDetailViewModel>().ReverseMap();
            CreateMap<GenreForAddDto, Genre>().ReverseMap();
            CreateMap<GenreForUpdateDto, Genre>().ReverseMap();

            // User Profile
            CreateMap<User, CreateUserDto>().ReverseMap();
        }
    }
}