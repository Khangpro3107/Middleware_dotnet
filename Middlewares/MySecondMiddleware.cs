using Middleware.Services;

namespace Middleware.Middlewares
{
    // is the same as MyFirstMiddleware, used to demonstrate the order of middlewares
    public class MySecondMiddleware : IMiddleware
    {
        public static int count;
        public ConsoleService ConsoleService;
        public MySecondMiddleware(ConsoleService consoleService)
        {
            count++;
            this.ConsoleService = consoleService;
            Console.WriteLine("MySecondMiddleware: " + count);
        }
        //}public MySecondMiddleware()
        //{
        //    count++;
        //    Console.WriteLine("MySecondMiddleware: " + count);
        //}
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("MySecondMiddleware InvokeAsync");
            await next(context);
        }
    }
}
