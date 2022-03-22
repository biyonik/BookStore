using BookStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Contexts.EntityFrameworkCore
{
    public class BookStoreDbContext: DbContext, IDbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> dbContextOptions): base(dbContextOptions)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) 
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}