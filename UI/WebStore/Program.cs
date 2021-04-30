using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WebStore
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();
        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            //.ConfigureLogging
            //((host, log) => log
            //        .AddConsole(opt => opt.IncludeScopes = true)
            //)
            .ConfigureWebHostDefaults(host => host
                .UseStartup<Startup>()
            )
            ;
    }
}
