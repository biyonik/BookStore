using AutoMapper;
using BookStore.API.Contexts.EntityFrameworkCore;
using BookStore.API.DataTransferObjects.Book;
using BookStore.API.Operations.Command;
using BookStore.API.Operations.Query;
using BookStore.API.Validators.FluentValidation;
using BookStore.API.ViewModels.Books;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BooksController: ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BooksController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<IActionResult> GetBooks() 
        {
            GetBooksQuery getBooksQuery = new GetBooksQuery(_context, _mapper);
            var bookList = await getBooksQuery.HandleAsync();
            return Ok(bookList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            BookDetailViewModel bookDetailViewModel;
            GetBookDetailQuery getBookDetailQuery = new GetBookDetailQuery(_context, _mapper);
            getBookDetailQuery.BookId = id;
            GetBookQueryValidator getBookQueryValidator = new GetBookQueryValidator();
            await getBookQueryValidator.ValidateAndThrowAsync(getBookDetailQuery);
            bookDetailViewModel = await getBookDetailQuery.HandleAsync();
            return Ok(bookDetailViewModel);
        }

        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] BookForAddDto entity) 
        {
            CreateBookCommand createBookCommand = new CreateBookCommand(_context, _mapper);
            createBookCommand.BookForAddDto = entity;
            CreateBookCommandValidator createBookCommandValidator = new CreateBookCommandValidator();
            await createBookCommandValidator.ValidateAndThrowAsync(createBookCommand);
            
            await createBookCommand.HandleAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookForUpdateDto entity) 
        {
            UpdateBookCommand updateBookCommand = new UpdateBookCommand(_context, _mapper);
            updateBookCommand.BookId = id;
            updateBookCommand.BookForUpdateDto = entity;
            UpdateBookCommandValidator updateBookCommandValidator = new UpdateBookCommandValidator();
            await updateBookCommandValidator.ValidateAndThrowAsync(updateBookCommand);
            await updateBookCommand.HandleAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteBookCommand deleteBookCommand = new DeleteBookCommand(_context);
            deleteBookCommand.BookId = id;
            DeleteBookCommandValidator deleteBookCommandValidator = new DeleteBookCommandValidator();
            await deleteBookCommandValidator.ValidateAndThrowAsync(deleteBookCommand);
            await deleteBookCommand.HandleAsync(); 
            return Ok();
        }
    }
}