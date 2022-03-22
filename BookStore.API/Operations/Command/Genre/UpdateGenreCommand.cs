
using AutoMapper;
using BookStore.API.Contexts;
using BookStore.API.DataTransferObjects.Genre;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Operations.Command.Genre
{
    public class UpdateGenreCommand
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;
        public int GenreId {get; set;}
        public GenreForUpdateDto GenreForUpdateDto {get; set;}

        public UpdateGenreCommand(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }   

        public async Task HandleAsync()
        {
            var genreIsExists = await _context.Genres.AsNoTracking().Where(x => x.Id == GenreId).SingleOrDefaultAsync();
            if(genreIsExists is null) 
            {
                throw new InvalidOperationException("Böyle bir tür kaydı bulunamadı");
            }
            if(_context.Genres.Any(x => x.Name.ToLower() == genreIsExists.Name.ToLower() && x.Id != GenreId))
            {
                throw new InvalidOperationException("Aynı isimli bir kitap türü zaten mevcut!");
            }

            var genre = _mapper.Map<Models.Genre>(GenreForUpdateDto);
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();
        }
    }
}