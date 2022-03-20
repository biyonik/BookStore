using BookStore.API.Operations.Command.Genre;
using FluentValidation;

namespace BookStore.API.Validators.FluentValidation
{
    public class DeleteGenreCommandValidator: AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(x => x.GenreId)
                .NotEmpty()
                    .WithMessage("GenreId boş bırakılamaz!")
                .GreaterThan(0)
                    .WithMessage("GenreId değeri 0'dan büyük olmak zorundadır!");
        }
    }
}