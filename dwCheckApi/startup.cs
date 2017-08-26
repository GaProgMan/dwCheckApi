using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


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
            services.AddCustomizedMvc();
            // TODO don't hard code this
            services.AddCorsPolicy("CorsPolicy");
            // TODO don't hard code this
            services.AddDbContext("Data Source=dwDatabase.db");
            services.AddTransientServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.EnsureDatabaseIsSeeded(false);
            }

            // TODO don't hard code this
            app.UseCorsPolicy("CorsPolicy");
            
            app.UseStaticFiles();
            app.UseCustomisedMvc();
        }
    }
}