
using BookStore.API.Services.Abstract;

namespace BookStore.API.Services.Concrete
{
    public class ConsoleLogger : ILoggerService
    {
        public void Write(string message)
        {
            Console.WriteLine($"[ConsoleLogger] {message}");
        }
    }
}