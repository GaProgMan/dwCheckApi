using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace dwCheckApi
{
    public class Program
    {
        #region .NET Core 1.0 Main
        //public static void Main(string[] args)
        //{
        //    var host = new WebHostBuilder()
        //      .UseKestrel()
        //      .UseContentRoot(Directory.GetCurrentDirectory())
        //      // .UseUrls("http://0.0.0.0:5002") //<-for docker
        //      //.UseUrls("http://0.0.0.0:5000") //<-for docker
        //      .UseIISIntegration()
        //      .UseStartup<Startup>()
        //      .Build();

        //    host.Run();
        //}
        #endregion
        
        #region .NET Core 2.0 Main
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
        #endregion
    }
}