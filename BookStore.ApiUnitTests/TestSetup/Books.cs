using System;
using BookStore.API.Contexts.EntityFrameworkCore;
using BookStore.API.Models;

namespace BookStore.ApiUnitTests.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext bookStoreDbContext) 
        {
            bookStoreDbContext.Books.AddRange(new Book[] {
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
        }
    }
}