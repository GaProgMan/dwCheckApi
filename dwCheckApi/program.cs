using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace dwCheckApi
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            
            host.Run();
        }

        public static IHost BuildWebHost(string[] args) =>
            // Notes for CreateDefaultBuilder:
            //    - loads IConfiguration from UserSecrets automatically when in Development env
            //    - still loads IConfiguration from appsettings[envName].json
            //    - adds Developer Exception page when in Development env
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                // might need this anyway
                .UseContentRoot(Directory.GetCurrentDirectory())
                .Build();
    }
}