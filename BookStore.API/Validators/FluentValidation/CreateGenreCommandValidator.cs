using BookStore.API.Operations.Command.Genre;
using FluentValidation;
namespace BookStore.API.Validators.FluentValidation
{
    public class CreateGenreCommandValidator: AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(command => command.GenreForAddDto.Name)
                .NotEmpty()
                    .WithMessage("Tür boş bırakılamaz!")
                .MinimumLength(2)
                    .WithMessage("Tür en az iki karakter uzunluğunda olmalıdır!");
        }
    }
}