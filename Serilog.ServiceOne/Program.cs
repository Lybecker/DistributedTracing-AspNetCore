using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Serilog.ServiceOne
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>()
                .UseUrls("http://*:5000");
    }
}
