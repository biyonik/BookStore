using AutoMapper;
using BookStore.API.Contexts.EntityFrameworkCore;
using BookStore.API.ViewModels.Books;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Operations.Query.Book
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBooksQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;    
            _mapper = mapper;
        }

        public async Task<List<BooksViewModel>> HandleAsync()
        {
            var bookList = await _context.Books.OrderByDescending(b => b.Id).ToListAsync<Models.Book>();
            List<BooksViewModel> booksViewModels = _mapper.Map<List<BooksViewModel>>(bookList);
            return booksViewModels;
        }
    }
}