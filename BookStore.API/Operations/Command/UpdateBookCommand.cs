using AutoMapper;
using BookStore.API.Contexts.EntityFrameworkCore;
using BookStore.API.DataTransferObjects.Book;
using BookStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Operations.Command
{
    public class UpdateBookCommand
    {
        public int BookId { get; set; }
        public BookForUpdateDto BookForUpdateDto { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateBookCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task HandleAsync()
        {
            var bookIsExists = await _context.Books.AsNoTracking().Where(x => x.Id == BookId).SingleOrDefaultAsync();
            if(bookIsExists is null) 
            {
                throw new InvalidOperationException("Böyle bir kitap kaydı bulunamadı");
            }
            var book = _mapper.Map<Book>(BookForUpdateDto);
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
        
    }
}