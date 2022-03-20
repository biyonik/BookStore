using BookStore.API.Operations.Query.Book;
using FluentValidation;

namespace BookStore.API.Validators.FluentValidation
{
    public class GetBookQueryValidator:AbstractValidator<GetBookDetailQuery>
    {
        public GetBookQueryValidator()
        {
            RuleFor(query => query.BookId).GreaterThan(0).WithMessage("BookId değeri 0'dan büyük olmak zorundadır");
        }
    }
}