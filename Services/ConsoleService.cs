namespace Middleware.Services
{
    public class ConsoleService
    {
        public static int count;
        public ConsoleService() {
            count++;
            Console.WriteLine("ConsoleService: " + count);
        }
    }
}
