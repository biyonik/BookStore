using BookStore.API.Contexts.EntityFrameworkCore;
using BookStore.API.ViewModels.Books;
using Microsoft.EntityFrameworkCore;
using BookStore.API.Enumerations;
using AutoMapper;

namespace BookStore.API.Operations.Query
{
    public class GetBookDetailQuery
    {
        public GetBookDetailQuery(int bookId) 
        {
            this.BookId = bookId;
   
        }
                public int BookId { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BookDetailViewModel> HandleAsync()
        {
            var book = await _context.Books.Where(x => x.Id == BookId).SingleOrDefaultAsync();
            if(book is null) 
            {
                throw new InvalidOperationException("Kitap bulunamadı!");
            }
            // BookDetailViewModel bookDetailViewModel = new BookDetailViewModel 
            // {
            //     Title = book.Title,
            //     Genre = ((GenreEnums)book.GenreId).ToString(),
            //     PageCount = book.PageCount,
            //     PublishDate = book.PublishDate.ToString("dd/MM/yyy")
            // };
            BookDetailViewModel bookDetailViewModel = _mapper.Map<BookDetailViewModel>(book);
            return bookDetailViewModel;
        }
    }
}