using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace AspNetMicroservices.Gateway.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // AppContext.SetSwitch(
                    //     "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

                    // if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
                    //     webBuilder.ConfigureKestrel(options =>
                    //     {
                    //         // Setup a HTTP/2 endpoint without TLS.
                    //         options.ListenLocalhost(5000, o => o.Protocols = 
                    //             HttpProtocols.Http2);
                    //     });
                    // }
                    webBuilder.UseStartup<Startup>();
                });
    }
}
