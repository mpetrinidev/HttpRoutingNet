using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core2_0
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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
