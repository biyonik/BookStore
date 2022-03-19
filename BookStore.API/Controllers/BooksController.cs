using BookStore.API.Contexts.EntityFrameworkCore;
using BookStore.API.DataTransferObjects.Book;
using BookStore.API.Models;
using BookStore.API.Operations.Command;
using BookStore.API.Operations.Query;
using BookStore.API.ViewModels.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BooksController: ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public BooksController(BookStoreDbContext context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetBooks() 
        {
            GetBooksQuery getBooksQuery = new GetBooksQuery(_context);
            var bookList = await getBooksQuery.HandleAsync();
            return Ok(bookList);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id) {
            BookDetailViewModel bookDetailViewModel;
            try 
            {
                GetBookDetailQuery getBookDetailQuery = new GetBookDetailQuery(_context);
                getBookDetailQuery.BookId = id;
                bookDetailViewModel = await getBookDetailQuery.HandleAsync();
            } catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            return Ok(bookDetailViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BookForAddDto entity) 
        {
            try 
            {
                CreateBookCommand createBookCommand = new CreateBookCommand(_context);
                createBookCommand.BookForAddDto = entity;
                await createBookCommand.HandleAsync();
                
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookForUpdateDto entity) 
        {
            try 
            {
                UpdateBookCommand updateBookCommand = new UpdateBookCommand(_context);
                updateBookCommand.BookId = id;
                updateBookCommand.BookForUpdateDto = entity;
                await updateBookCommand.HandleAsync();
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try 
            {
                DeleteBookCommand deleteBookCommand = new DeleteBookCommand(_context);
                deleteBookCommand.BookId = id;
                await deleteBookCommand.HandleAsync(); 
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}