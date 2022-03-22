using BookStore.API.Contexts;
using BookStore.API.Contexts.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Operations.Command.Book
{
    public class DeleteBookCommand
    {
        private readonly IDbContext _context;
        public int BookId { get; set; }
        public DeleteBookCommand(IDbContext context)
        {
            _context = context;
        }

        public async Task HandleAsync()
        {
            var book = await _context.Books.Where(x => x.Id == BookId).SingleOrDefaultAsync();
            if(book is null) 
            {
                throw new InvalidOperationException("Böyle bir kitap bulunamadı!");
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}