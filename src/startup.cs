using dwCheckApi.DatabaseContexts;
using dwCheckApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;

namespace dwCheckApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
			services.AddCors(options =>
		{
			options.AddPolicy("CorsPolicy",
				builder => builder.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader()
				.AllowCredentials());
		});

            // Give ourselves access to the DwContext
            services.AddDbContext<DwContext>(options =>
                //options.UseSqlite(Configuration.GetConnectionString("dwCheckApiConnection")));
                options.UseSqlite(Configuration["Data:SqliteConnection:ConnectionString"]));

            // DI our services in
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<ICharacterService, CharacterService>();
            services.AddTransient<ISeriesService, SeriesService>();
            services.AddTransient<IDatabaseService, DatabaseService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

	        app.UseCors("CorsPolicy");
            app.UseMvc();

            // Commented out because each time dotnet run is issued,
            // this is ignore. Is dotnet run building in release mode?
            //if (env.IsDevelopment())
            //{
                // seed the database using an extension method
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<DwContext>();       
                //    context.Database.Migrate();
                    context.EnsureSeedData();
                }
            //}
        }
    }
}