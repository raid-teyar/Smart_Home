using SecuritySystem.Server.Services;
using SecuritySystem.UI.Models;

namespace SecuritySystem.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
          
            
            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");


            app.Run();

        }
    }
}