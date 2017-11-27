using System.IO;
using System.Linq;
using ClacksMiddleware.Extensions;
using dwCheckApi.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;


namespace dwCheckApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                    {
                        "text/plain", "application/json"
                    });
            });
            
            services.AddCustomizedMvc();
            services.AddCorsPolicy();
            services.AddDbContext();
            services.AddTransientServices();
            
            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc($"v{CommonHelpers.GetVersionNumber()}",
                    new Info
                    {
                        Title = "dwCheckApi",
                        Version = $"v{CommonHelpers.GetVersionNumber()}",
                        Description = "A simple APi to get the details on Books, Characters and Series within a canon of novels",
                        Contact = new Contact
                        {
                            Name = "Jamie Taylor",
                            Email = "",
                            Url = "https://dotnetcore.gaprogman.com"
                        }
                    }
                );
                // Set the comments path for the Swagger JSON and UI.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "dwCheckApi.xml"); 
                c.IncludeXmlComments(xmlPath); 
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.EnsureDatabaseIsSeeded(false);
            }
            
            app.UseResponseCompression();
            app.GnuTerryPratchett();
            app.UseCorsPolicy();
            app.UseStaticFiles();
            
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v{CommonHelpers.GetVersionNumber()}/swagger.json",
                    $"dwCheckApi {CommonHelpers.GetVersionNumber()}");
            });
            
            app.UseCustomisedMvc();
        }
    }
}