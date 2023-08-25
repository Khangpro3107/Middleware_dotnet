using Middleware.Services;

namespace Middleware.Middlewares
{
    // a basic middleware with its dep + count to keep track of the number of instances instantiated
    public class MyFirstMiddleware : IMiddleware
    {
        public static int count;
        public ConsoleService ConsoleService;
        public MyFirstMiddleware(ConsoleService consoleService)
        {
            count++;
            this.ConsoleService = consoleService;
            Console.WriteLine("MyFirstMiddleware: " + count);
        }
        //}public MyFirstMiddleware()
        //{
        //    count++;
        //    Console.WriteLine("MyFirstMiddleware: " + count);
        //}
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("MyFirstMiddleware InvokeAsync");
            await next(context);
            Console.WriteLine("MyFirstMiddleware InvokeAsync after next");
        }
    }
}
