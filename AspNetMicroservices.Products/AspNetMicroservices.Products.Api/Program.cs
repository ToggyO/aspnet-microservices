using System.Runtime.InteropServices;

using AspNetMicroservices.Logging.Serilog;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;

namespace AspNetMicroservices.Products.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
	            .UseSerilogLogging()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    {
                        webBuilder.UseKestrel(options =>
                            options.ListenLocalhost(5002, o =>
                                o.Protocols = HttpProtocols.Http2));
                    }
                    webBuilder.UseStartup<Startup>();
                });
    }
}
