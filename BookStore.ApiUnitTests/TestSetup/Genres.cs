using BookStore.API.Contexts.EntityFrameworkCore;
using BookStore.API.Models;

namespace BookStore.ApiUnitTests.TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this BookStoreDbContext bookStoreDbContext)
        {
            bookStoreDbContext.Genres.AddRange(new Genre[] {
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
        }
    }
}