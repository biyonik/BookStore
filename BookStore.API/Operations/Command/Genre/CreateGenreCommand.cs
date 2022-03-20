using AutoMapper;
using BookStore.API.Contexts.EntityFrameworkCore;
using BookStore.API.DataTransferObjects.Genre;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Operations.Command.Genre
{
    public class CreateGenreCommand
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GenreForAddDto GenreForAddDto {get; set;}

        public CreateGenreCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task HandleAsync()
        {
            var genreIsExists = await _context.Genres.SingleOrDefaultAsync(x => x.Name == GenreForAddDto.Name);
            if(genreIsExists is not null) 
            {
                throw new InvalidOperationException("Bu tür daha önce kaydedilmiş!");
            }
            genreIsExists = new Models.Genre();
            genreIsExists.Name = GenreForAddDto.Name;
            await _context.Genres.AddAsync(genreIsExists);
            await _context.SaveChangesAsync();
        }

    }
}