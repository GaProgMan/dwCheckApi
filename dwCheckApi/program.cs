using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace dwCheckApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            // Notes for CreateDefaultBuilder:
            //    - loads IConfiguration from UserSecrets automatically when in Development env
            //    - still loads IConfiguration from appsettings[envName].json
            //    - adds Developer Exception page when in Development env
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                // might need this anyway
                .UseContentRoot(Directory.GetCurrentDirectory())
                .Build();
    }
}