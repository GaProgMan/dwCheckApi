using System;
using System.IO;
using System.Reflection;
using dwCheckApi.Common;
using dwCheckApi.DAL;
using dwCheckApi.Helpers;
using dwCheckApi.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace dwCheckApi
{
    /// <summary>
    /// This class is based on some of the suggestions bty K. Scott Allen in
    /// his NDC 2017 talk https://www.youtube.com/watch?v=6Fi5dRVxOvc
    /// </summary>
    public static class ConfigureContainerExtensions
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

        public static void AddCorsPolicy(this IServiceCollection serviceCollection, string corsPolicyName = null)
        {
            serviceCollection.AddCors(options =>
            {
                options.AddPolicy(corsPolicyName ?? CorsPolicyName,
                    builder =>
                        builder.WithOrigins("localhost")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials());
            });
        }

        /// <summary>
        /// Used to register and add the Swagger generator to the service Collection
        /// </summary>
        /// <param name="serviceCollection">
        /// The <see cref="IServiceCollection"/> which is used in the Container
        /// </param>
        /// <param name="versionNumberString">The version number for the application</param>
        /// <param name="includeXmlDocumentation">
        /// Whether or not to include XmlDocumentation (defaults to True)
        /// </param>
        /// <remarks>
        /// includeXmlDocumentation requires:
        ///   <PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Debug|AnyCPU'">
        ///     <DocumentationFile>bin\Debug\net6.0\dwCheckApi.xml</DocumentationFile>
        ///  </PropertyGroup>
        /// for debug builds and:
        ///   <PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Release|AnyCPU'">
        ///     <DocumentationFile>bin\Release\net6.0\dwCheckApi.xml</DocumentationFile>
        ///  </PropertyGroup>
        /// </remarks>
        public static void AddSwagger(this IServiceCollection serviceCollection, string versionNumberString,
            bool includeXmlDocumentation = true)
        {
            // Register the Swagger generator, defining one or more Swagger documents
            serviceCollection.AddSwaggerGen(options =>
            {
                options.SwaggerDoc($"v{CommonHelpers.GetVersionNumber()}",
                    new OpenApiInfo
                    {
                        Title = "dwCheckApi",
                        Version = $"v{CommonHelpers.GetVersionNumber()}",
                        Description = "A simple APi to get the details on Books, Characters and Series within a canon of novels",
                        Contact = new OpenApiContact
                        {
                            Name = "Jamie Taylor",
                            Email = "",
                            Url = new Uri("https://dotnetcore.show")
                        }
                    }
                );
                
                if (!includeXmlDocumentation) return;
                // Set the comments path for the Swagger JSON and UI.
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }
    }
}