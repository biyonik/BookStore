using BookStore.API.Operations.Query.Genre;
using FluentValidation;

namespace BookStore.API.Validators.FluentValidation
{
    public class GetGenreQueryValidator: AbstractValidator<GetGenreDetailQuery>
    {
        public GetGenreQueryValidator()
        {
            RuleFor(query => query.GenreId).GreaterThan(0).WithMessage("GenreId değeri 0'dan büyük olmak zorundadır");
        }
    }
}