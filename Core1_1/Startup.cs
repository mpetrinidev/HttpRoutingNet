using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Core1_1
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApiVersioning(_ =>
            {
                _.ReportApiVersions = true;
                _.DefaultApiVersion = new ApiVersion(1, 0);
                _.AssumeDefaultVersionWhenUnspecified = true;
                _.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("version"),
                    new HeaderApiVersionReader()
                    {
                        HeaderNames = { "x-api-version" }
                    });
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(_ =>
            {
                _.MapRoute(
                    name: "get-values",
                    template: "{id:int?}/{*catchall}",
                    defaults: new { controller = "Values", action = "Get" });

                _.MapRoute(
                    name: "aou-values",
                    template: "actions/addorupdate/{type:bool}",
                    defaults: new { controller = "Values", action = "AddOrUpdate" });

                _.MapRoute(
                    name: "delete-values",
                    template: "actions/delete",
                    defaults: new { controller = "Values", action = "Delete" });
            });
        }
    }
}
