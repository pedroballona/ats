using HR.ATS.Infrastructure;
using HR.ATS.WebAPI.Configurations;
using HR.ATS.WebAPI.Middleware;
using HR.ATS.WebAPI.Security.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tnf.Configuration;

namespace HR.ATS.WebAPI
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
            services.AddSingleton(new RacConfiguration(Configuration));
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
            services.AddAutoSwaggerSetup();
            services.AddMvcCore().AddNewtonsoftJson();

            services.AddCorsAll("AllowAll")
                    .AddTnfAspNetCore()
                    .AddTnfAspNetCoreSecurity(Configuration)
                    .AddRacAuthorizationPolicy()
                    .AddControllers();

            services.AddInfrastructure();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseCors(
                builder => builder.AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader()
                                  .WithExposedHeaders("Content-Disposition", "File-Name")
            );

            app.UseTnfAspNetCore(
                options =>
                {
                    //options.ConfigureLocalization();
                    options.MultiTenancy(tenant => tenant.IsEnabled = true);
                }
            );

            app.UseTnfAspNetCoreSecurity(
                config =>
                {
                    config.RolesLocalization.UseEmbeddedJsonFiles(
                        "SeedRoles",
                        typeof(RolesConstants).Assembly,
                        "HR.ATS.WebAPI.Security.Roles"
                    );
                }
            );

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwaggerConfigurations();

            app.UseStaticFiles();
            if (!env.IsDevelopment()) app.UseSpaStaticFiles();


            app.UseRouting();
            app.UseAuthorization();

            app.UseAutomaticPersonCreation();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSpa(
                spa =>
                {
                    // To learn more about options for serving an Angular SPA from ASP.NET Core,
                    // see https://go.microsoft.com/fwlink/?linkid=864501

                    spa.Options.SourcePath = "ClientApp";

                    if (env.IsDevelopment()) spa.UseAngularCliServer("start");
                }
            );
        }
    }
}