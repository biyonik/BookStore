using BookStore.API.Operations.Command.Book;
using FluentValidation;

namespace BookStore.API.Validators.FluentValidation
{
    public class UpdateBookCommandValidator: AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0).WithMessage("BookId değeri 0'dan büyük olmak zorundadır!");
            RuleFor(command => command.BookForUpdateDto.GenreId).GreaterThan(0).WithMessage("GenreId değeri 0'dan büyük olmalıdır!");
            RuleFor(command => command.BookForUpdateDto.Title)
                .NotEmpty()
                    .WithMessage("Title değeri boş bırakılamaz!")
                .MinimumLength(2)
                    .WithMessage("Title en az 2 karakter uzunluğunda olmalıdır!");
            RuleFor(command => command.BookForUpdateDto.PageCount).GreaterThan(0);
            RuleFor(command => command.BookForUpdateDto.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
        }
    }
}