using Middleware.Services;

namespace Middleware.Middlewares
{
    // this middleware is created as a 'conventional middleware'
    public class ConventionalMiddleware
    {
        public static int count;
        private readonly RequestDelegate _next;
        //private readonly ConsoleService _consoleService;

        public ConventionalMiddleware(RequestDelegate next)
        {
            _next = next;
            count++;
            Console.WriteLine("ConventionalMiddleware: " + count);
        }
        //}public ConventionalMiddleware(RequestDelegate next, ConsoleService consoleService)
        //{
        //    _next = next;
        //    this._consoleService = consoleService;
        //}

        //public Task Invoke(HttpContext httpContext)
        //{
        //    Console.WriteLine("Conventional Invoke");
        //    return _next(httpContext);
        //}

        //public async Task InvokeAsync(HttpContext httpContext)
        //{
        //    Console.WriteLine("Conventional InvokeAsync");
        //    await _next(httpContext);
        //}
    }
}
