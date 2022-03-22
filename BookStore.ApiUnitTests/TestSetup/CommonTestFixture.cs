using AutoMapper;
using BookStore.API.Contexts.EntityFrameworkCore;
using BookStore.API.Mappings.AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace BookStore.ApiUnitTests.TestSetup
{
    public class CommonTestFixture
    {
        public BookStoreDbContext _context {get; set;}
        public IMapper _mapper { get; set; }

        public CommonTestFixture() 
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName: "BookStoreTestDb").Options;
            _context = new BookStoreDbContext(options);
            _context.Database.EnsureCreated();
            _context.AddBooks();
            _context.AddGenres();
            _context.SaveChanges();

            _mapper = new MapperConfiguration(config => 
                {
                    config.AddProfile<MappingProfile>(); 
                })
                .CreateMapper();
        }        
    }
}