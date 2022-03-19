
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BookStore.API.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            try
            {
                string message = $"[Request] HTTP {context.Request.Method} - {context.Request.Path}";
                System.Console.WriteLine(message);
                await _next(context);
                watch.Stop();
                message = $"[Response] HTTP {context.Request.Method} - {context.Request.Path} responded {context.Response.StatusCode} in {watch.Elapsed.TotalMilliseconds} ms";
                System.Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleExceptionAsync(context, ex, watch);
            }
           
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, Stopwatch watch)
        {
            string message = $"[Error] HTTP {context.Request.Method} - {context.Response.StatusCode} Error Message: {ex.Message} in {watch.Elapsed.TotalMilliseconds} ms";
            Console.WriteLine(message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonSerializer.Serialize(new {
                error = ex.Message,
            });
            await context.Response.WriteAsync(result);
        }
    }
}