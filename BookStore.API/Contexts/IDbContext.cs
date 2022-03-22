using BookStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Contexts
{
    public interface IDbContext
    {
        DbSet<Book> Books {get; set;}
        DbSet<Genre> Genres {get; set;}

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();
    }
}