using AutoMapper;
using BookStore.API.Contexts.EntityFrameworkCore;
using BookStore.API.ViewModels.Genres;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Operations.Query.Genre
{
    public class GetGenresQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenresQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GenreViewModel>> HandleAsync()
        {
            var genres = await _context.Genres.Where(x => x.IsActive).ToListAsync();
            List<GenreViewModel> genreViewModels = _mapper.Map<List<GenreViewModel>>(genres);
            return genreViewModels;
        }
    }
}