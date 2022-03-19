using BookStore.API.Contexts.EntityFrameworkCore;
using BookStore.API.DataTransferObjects.Book;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Operations.Command
{
    public class UpdateBookCommand
    {
        public int BookId { get; set; }
        public BookForUpdateDto BookForUpdateDto { get; set; }
        private readonly BookStoreDbContext _context;
        public UpdateBookCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public async Task HandleAsync()
        {
            var bookIsExists = await _context.Books.AsNoTracking().Where(x => x.Id == BookId).SingleOrDefaultAsync();
            if(bookIsExists is null) 
            {
                throw new InvalidOperationException("Böyle bir kitap kaydı bulunamadı");
            }
            bookIsExists.GenreId = BookForUpdateDto.GenreId;
            bookIsExists.Title = BookForUpdateDto.Title;
            bookIsExists.PageCount = BookForUpdateDto.PageCount;
            bookIsExists.PublishDate = BookForUpdateDto.PublishDate;
            _context.Books.Update(bookIsExists);
            await _context.SaveChangesAsync();
        }
        
    }
}