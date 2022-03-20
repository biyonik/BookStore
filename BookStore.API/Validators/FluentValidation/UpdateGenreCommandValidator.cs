using BookStore.API.Operations.Command.Genre;
using FluentValidation;

namespace BookStore.API.Validators.FluentValidation
{
    public class UpdateGenreCommandValidator: AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command => command.GenreId).GreaterThan(0).WithMessage("BookId değeri 0'dan büyük olmak zorundadır!");
            RuleFor(command => command.GenreForUpdateDto.Name)
                .MinimumLength(2)
                .When(x => x.GenreForUpdateDto.Name.Trim() != string.Empty)
                    .WithMessage("Name en az 2 karakter uzunluğunda olmalıdır!");
        }
    }
}