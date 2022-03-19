using BookStore.API.Operations.Command;
using FluentValidation;

namespace BookStore.API.Validators.FluentValidation
{
    public class CreateBookCommandValidator: AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(command => command.BookForAddDto.GenreId).GreaterThan(0).WithMessage("GenreId değeri 0'dan büyük olmalıdır!");
            RuleFor(command => command.BookForAddDto.Title)
                .NotEmpty()
                    .WithMessage("Title boş bırakılamaz!")
                .MinimumLength(2)
                    .WithMessage("Title en az 2 karakter uzunluğunda olmalıdır");
            RuleFor(command => command.BookForAddDto.PageCount).GreaterThan(0);
            RuleFor(command => command.BookForAddDto.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
        }    
    }
}