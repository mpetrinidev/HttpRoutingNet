using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core2_1
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

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
