using AutoMapper;
using BookStore.API.Contexts.EntityFrameworkCore;
using BookStore.API.DataTransferObjects.Genre;
using BookStore.API.Operations.Command.Genre;
using BookStore.API.Operations.Query.Genre;
using BookStore.API.Validators.FluentValidation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class GenresController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenresController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<IActionResult> GetGenres()
        {
            GetGenresQuery getGenresQuery = new GetGenresQuery(_context, _mapper);
            var genresList = await getGenresQuery.HandleAsync();
            return Ok(genresList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            GetGenreDetailQuery getGenreDetailQuery = new GetGenreDetailQuery(_context, _mapper);
            getGenreDetailQuery.GenreId = id;
            GetGenreQueryValidator getGenreQueryValidator = new GetGenreQueryValidator();
            await getGenreQueryValidator.ValidateAndThrowAsync(getGenreDetailQuery);
            var genre = await getGenreDetailQuery.HandleAsync();
            return Ok(genre);
        }

        [HttpPost()]
        public async Task<IActionResult> Add([FromBody] GenreForAddDto entity)
        {
            CreateGenreCommand createGenreCommand = new CreateGenreCommand(_context, _mapper);
            createGenreCommand.GenreForAddDto = entity;
            CreateGenreCommandValidator createGenreCommandValidator = new CreateGenreCommandValidator();
            await createGenreCommandValidator.ValidateAndThrowAsync(createGenreCommand);
            
            await createGenreCommand.HandleAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GenreForUpdateDto entity) 
        {
            UpdateGenreCommand updateGenreCommand = new UpdateGenreCommand(_context, _mapper);
            updateGenreCommand.GenreId = id;
            updateGenreCommand.GenreForUpdateDto = entity;
            UpdateGenreCommandValidator updateGenreCommandValidator = new UpdateGenreCommandValidator();
            await updateGenreCommandValidator.ValidateAndThrowAsync(updateGenreCommand);
            await updateGenreCommand.HandleAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteGenreCommand deleteGenreCommand = new DeleteGenreCommand(_context);
            deleteGenreCommand.GenreId = id;
            DeleteGenreCommandValidator deleteGenreCommandValidator = new DeleteGenreCommandValidator();
            await deleteGenreCommandValidator.ValidateAndThrowAsync(deleteGenreCommand);
            await deleteGenreCommand.HandleAsync(); 
            return Ok();
        }
    }
}