using BookStore.API.ViewModels.Books;

namespace BookStore.API.ViewModels.Genres
{
    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<BooksViewModel> Books {get; set;}
    }
}   