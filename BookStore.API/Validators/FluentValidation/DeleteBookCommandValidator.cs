using BookStore.API.Operations.Command.Book;
using FluentValidation;

namespace BookStore.API.Validators.FluentValidation
{
    public class DeleteBookCommandValidator: AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0).WithMessage("BookId değeri 0'dan büyük olmak zorundadır!");
        }
    }
}