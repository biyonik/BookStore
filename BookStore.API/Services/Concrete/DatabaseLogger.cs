

using BookStore.API.Services.Abstract;

namespace BookStore.API.Services.Concrete
{
    public class DatabaseLogger : ILoggerService
    {
        public void Write(string message)
        {
            Console.WriteLine($"[DatabaseLogger] {message}");
        }
    }
}