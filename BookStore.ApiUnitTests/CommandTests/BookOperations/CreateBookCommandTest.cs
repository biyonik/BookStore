using System;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.API.Contexts.EntityFrameworkCore;
using BookStore.API.DataTransferObjects.Book;
using BookStore.API.Models;
using BookStore.API.Operations.Command.Book;
using BookStore.ApiUnitTests.TestSetup;
using FluentAssertions;
using Xunit;

namespace BookStore.ApiUnitTests.CommandTests.BookOperations
{
    public class CreateBookCommandTest: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandTest(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture._context;
            _mapper = commonTestFixture._mapper;
        }

        [Fact]
        public async Task WhenAlreadyExistsBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
             // arrange = hazırlık
             var book = new Book {
                 Title = "Test_WhenAlreadyExistsBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",
                 PageCount = 100,
                 PublishDate = new System.DateTime(1990, 01, 10),
                 GenreId = 2
             };
             _context.Books.Add(book);
             await _context.SaveChangesAsync();

             CreateBookCommand createBookCommand = new CreateBookCommand(_context, _mapper);
             createBookCommand.BookForAddDto = new BookForAddDto {
                 Title = book.Title
             };

            // act & assert = çalıştırma - doğrulama

            Func<Task> action = async () => {
                await createBookCommand.HandleAsync();
            };

            // await FluentActions
            //     .Invoking(async () => await createBookCommand.HandleAsync())
            //     .Should().ThrowAsync<InvalidOperationException>();

            await action.Should().ThrowAsync<InvalidOperationException>().WithMessage("Bu kitap zaten eklenmiş!");
                
             // assert = doğrulama
        }
    }
}