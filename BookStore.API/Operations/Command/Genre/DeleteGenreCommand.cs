using BookStore.API.Contexts;
using BookStore.API.Contexts.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Operations.Command.Genre
{
    public class DeleteGenreCommand
    {
        public int GenreId {get; set;}
        private readonly IDbContext _context;

        public DeleteGenreCommand(IDbContext context)
        {
            _context = context;
        }  

        public async Task HandleAsync()
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(x => x.Id == GenreId);
            if(genre is null)
            {
                throw new InvalidOperationException("GenreId boş bırakılamaz!");
            }
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }
    }
}