using dwCheckApi.Common;
using dwCheckApi.DAL;
using dwCheckApi.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace dwCheckApi
{
    /// <summary>
    /// This class is based on some of the suggestions bty K. Scott Allen in
    /// his NDC 2017 talk https://www.youtube.com/watch?v=6Fi5dRVxOvc
    /// </summary>
    public static class ConfigureContainerExtenstions
    {
        private static string DbConnectionString => new DatabaseConfiguration().GetDatabaseConnectionString();
        private static string CorsPolicyName => new CorsConfiguration().GetCorsPolicyName();

        public static void AddDbContext(this IServiceCollection serviceCollection,
            string connectionString = null)
        {
            serviceCollection.AddDbContext<DwContext>(options =>
                options.UseSqlite(connectionString ?? DbConnectionString));
        }

        public static void AddTransientServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IBookService, BookService>();
            serviceCollection.AddTransient<ICharacterService, CharacterService>();
            serviceCollection.AddTransient<ISeriesService, SeriesService>();
            serviceCollection.AddTransient<IDatabaseService, DatabaseService>();
        }

        public static void AddCustomizedMvc(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMvc();
        }

        public static void AddCorsPolicy(this IServiceCollection serviceCollection, string corsPolicyName = null)
        {
            serviceCollection.AddCors(options =>
            {
                options.AddPolicy(corsPolicyName ?? CorsPolicyName,
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }
    }
}