using BookStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Contexts.EntityFrameworkCore
{
    public class BookStoreDbContext: DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> dbContextOptions): base(dbContextOptions)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}