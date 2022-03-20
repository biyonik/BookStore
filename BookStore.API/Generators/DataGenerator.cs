using BookStore.API.Contexts.EntityFrameworkCore;
using BookStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Generators
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>());
            if (context.Books.Any())
            {
                return;
            }
            
            context.Genres.AddRange(new Genre[] {
                new Genre
                {
                    Name = "Personal Growth"
                },
                new Genre
                {
                    Name = "Science Fiction"
                },
                new Genre
                {
                    Name = "Romance"
                }
            });

            context.Books.AddRange(new Book[] {
                new Book {
                    Title="Lean Startup",
                    GenreId=1, // Personal Growth
                    PageCount = 200,
                    PublishDate = new DateTime(2001,06,12)
                },
                new Book {
                    Title="Herland",
                    GenreId=2, // Science Fiction
                    PageCount = 250,
                    PublishDate = new DateTime(2010,05,23)
                },
                new Book {
                    Title="Dune",
                    GenreId=2, // Science Fiction
                    PageCount = 540,
                    PublishDate = new DateTime(2002,12,21)
                }
            });
            context.SaveChanges();
        }
    }
}