using dwCheckApi.DatabaseContexts;
using dwCheckApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace dwCheckApi
{
    public class Startup
    {
        #region .NET Core 1.0 version of Constructor
        //public Startup(IHostingEnvironment env)
        //{
            //var builder = new ConfigurationBuilder()
                //.SetBasePath(env.ContentRootPath)
                //.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                //.AddEnvironmentVariables();
            //Configuration = builder.Build();
        //}
        
        //public IConfigurationRoot Configuration { get; }
        
        #endregion

        #region .NET Core 2.0 version of constructor
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        #endregion
        
        #region .NET Core 1.0 version of ConfigureServices
        // This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{
            // Add framework services.
            //services.AddMvc();
            //services.AddCors(options =>
            //{
                //options.AddPolicy("CorsPolicy",
                    //builder => builder.AllowAnyOrigin()
                    //.AllowAnyMethod()
                    //.AllowAnyHeader()
                    //.AllowCredentials());
            //});
        
        
            // Give ourselves access to the DwContext
            //services.AddDbContext<DwContext>(options =>
                //options.UseSqlite(Configuration["Data:SqliteConnection:ConnectionString"]));
        
            // DI our services in
            //services.AddTransient<IBookService, BookService>();
            //services.AddTransient<ICharacterService, CharacterService>();
            //services.AddTransient<ISeriesService, SeriesService>();
            //services.AddTransient<IDatabaseService, DatabaseService>();
        //}
        #endregion

        #region .NET Core 2.0 version of ConfigureServices
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
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
                options.UseSqlite(Configuration["Data:SqliteConnection:ConnectionString"]));

            // DI our services in
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<ICharacterService, CharacterService>();
            services.AddTransient<ISeriesService, SeriesService>();
            services.AddTransient<IDatabaseService, DatabaseService>();
        }
        #endregion

        #region .NET Core 1.0 version of Configure
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
//        {
//            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
//            loggerFactory.AddDebug();
//
//            app.UseForwardedHeaders(new ForwardedHeadersOptions
//            {
//                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
//            });
//
//	        app.UseCors("CorsPolicy");
//            app.UseMvc();
//
//            // Commented out because each time dotnet run is issued,
//            // this is ignore. Is dotnet run building in release mode?
//            //if (env.IsDevelopment())
//            //{
//                // seed the database using an extension method
//                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
//                {
//                    var context = serviceScope.ServiceProvider.GetService<DwContext>();       
//                //    context.Database.Migrate();
//                    context.EnsureSeedData();
//                }
//            //}
//        }
        #endregion
        
        #region .NET Core 2.0 version of Configure
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseMvc();

            if (env.IsDevelopment())
            {
                // seed the database using an extension method
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<DwContext>();       
                    //context.Database.Migrate();
                    context.EnsureSeedData();
                }
            }
        }
        #endregion
    }
}