using AutoMapper;
using BookStore.API.Contexts.EntityFrameworkCore;
using BookStore.API.Operations.Command.Book;
using BookStore.API.Validators.FluentValidation;
using BookStore.ApiUnitTests.TestSetup;
using FluentAssertions;
using Xunit;

namespace BookStore.ApiUnitTests.CommandTests.BookOperations
{
    public class CreateBookCommandValidatorTest
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandValidatorTest(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture._context;
            _mapper = commonTestFixture._mapper;
        }

        [Theory]
        [InlineData("Lord of the", 0, 0)]
        [InlineData("Lord of the", 0, 2)]
        [InlineData("", -2, -2)]
        [InlineData("a", -2, -2)]
        [InlineData("Lord of the Rings", 1100, 2)]
        public void WhenInvalidInputsAreGiven_ValidatorShouldBeReturnErrors(string title, int pageCount, int genreId)
        {
            // arrange
            CreateBookCommand createBookCommand = new CreateBookCommand(null, null);
            createBookCommand.BookForAddDto = new API.DataTransferObjects.Book.BookForAddDto {
                Title = title,
                PageCount = pageCount,
                GenreId = genreId,
                PublishDate = System.DateTime.Now.Date.AddYears(-1)
            };
            
            //act
            CreateBookCommandValidator createBookCommandValidator = new CreateBookCommandValidator();
            var validationResult = createBookCommandValidator.Validate(createBookCommand);
            
            //assert
            validationResult.Errors.Count.Should().BeGreaterThan(0);
        }
        
    }
}