using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.API.Contexts.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Operations.Command
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _context;
        public int BookId { get; set; }
        public DeleteBookCommand(BookStoreDbContext context)
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