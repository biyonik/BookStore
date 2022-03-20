using AutoMapper;
using BookStore.API.Contexts.EntityFrameworkCore;
using BookStore.API.ViewModels.Genres;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Operations.Query.Genre
{
    public class GetGenreDetailQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int GenreId {get; set;}

        public GetGenreDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenreDetailViewModel> HandleAsync()
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(x => x.IsActive && x.Id ==GenreId);
            if(genre is null)
            {
                throw new InvalidOperationException("Kitap türü bulunamadı!");
            }
            GenreDetailViewModel genreDetailViewModel = _mapper.Map<GenreDetailViewModel>(genre);
            return genreDetailViewModel;
        }
    }
}