namespace Middleware.Middlewares
{
    // this middleware's ctor requires a param
    public class ParameterizedMiddleware: IMiddleware
    {
        public ParameterizedMiddleware(string postfix)
        {
            Console.WriteLine("ParameterizedMiddleware " + postfix);
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await next(context);
        }
    }
}
