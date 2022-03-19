using AutoMapper;
using BookStore.API.Contexts.EntityFrameworkCore;
using BookStore.API.DataTransferObjects.Book;
using BookStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Operations.Command
{
    public class CreateBookCommand
    {
        public BookForAddDto BookForAddDto { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task HandleAsync()
        {
            var book = await _context.Books.SingleOrDefaultAsync(x => x.Title == BookForAddDto.Title);
            if(book is not null) {
                throw new InvalidOperationException("Bu kitap zaten eklenmiş!");
            }
            // book = new Book {
            //     Title = BookForAddDto.Title,
            //     GenreId = BookForAddDto.GenreId,
            //     PageCount = BookForAddDto.PageCount,
            //     PublishDate = BookForAddDto.PublishDate
            // };
            book = _mapper.Map<Book>(BookForAddDto);
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }
    }
}