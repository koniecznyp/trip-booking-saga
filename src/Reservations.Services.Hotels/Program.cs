using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Reservations.Services.Hotels
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseDefaultServiceProvider(options => 
                    options.ValidateScopes = false)
                .UseStartup<Startup>();
    }
}
