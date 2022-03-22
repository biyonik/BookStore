using AutoMapper;
using BookStore.API.Contexts;
using BookStore.API.Contexts.EntityFrameworkCore;
using BookStore.API.DataTransferObjects.Book;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Operations.Command.Book
{
    public class CreateBookCommand
    {
        public BookForAddDto BookForAddDto { get; set; }
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommand(IDbContext context, IMapper mapper)
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
            book = _mapper.Map<Models.Book>(BookForAddDto);
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }
    }
}