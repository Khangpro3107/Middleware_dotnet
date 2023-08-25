using Middleware.Services;

namespace Middleware.Middlewares
{
    // ignore this file as it is not used
    public class MyExtensionMiddleware : IMiddleware
    {
        public static int count;
        public MyExtensionMiddleware()
        {
            count++;
            Console.WriteLine("MyExtensionMiddleware: " + count);
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("MyExtensionMiddleware InvokeAsync");
            await next(context);
        }
    }
}
