using BookStore.API.Contexts.EntityFrameworkCore;
using BookStore.API.Enumerations;
using BookStore.API.Models;
using BookStore.API.ViewModels.Books;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Operations.Query
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _context;
        public GetBooksQuery(BookStoreDbContext context)
        {
            _context = context;    
        }

        public async Task<List<BooksViewModel>> HandleAsync()
        {
            var bookList = await _context.Books.OrderByDescending(b => b.Id).ToListAsync<Book>();
            List<BooksViewModel> booksViewModels = new List<BooksViewModel>();
            foreach(var book in bookList) 
            {
                booksViewModels.Add(new BooksViewModel {
                    Title = book.Title,
                    Genre = ((GenreEnums)book.GenreId).ToString(),
                    PageCount = book.PageCount,
                    PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy")
                });
            }
            return booksViewModels;
        }
    }
}