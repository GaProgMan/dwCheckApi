using Microsoft.AspNetCore.Hosting;

namespace dwCheckApi
{
    public static class HostingEnvironmentExtensions
    {
        /// <summary>
        /// Used to calculate whether the application is running in either Production or Staging
        /// </summary>
        /// <param name="env">
        /// The <see cref="IHostingEnvironment"/> for the application
        /// </param>
        public static bool IsProdOrStaging(this IHostingEnvironment env)
        {
            return env.IsProduction() || env.IsStaging();
        }
    }
}