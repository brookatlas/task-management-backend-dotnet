using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using task_management_backend_dotnet.Models;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using task_management_backend_dotnet.Services;

namespace task_management_backend_dotnet
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
            services.AddControllers();
            services.AddScoped<TaskMangementDatabaseSettings>(
                provider => new TaskMangementDatabaseSettings(
                    System.Environment.GetEnvironmentVariable("CONNECTION_STRING"),
                    System.Environment.GetEnvironmentVariable("DB_COLLECTION"),
                    System.Environment.GetEnvironmentVariable("DB_NAME")
                )
            );
            services.AddScoped<ProjectService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "task_management_backend_dotnet", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "task_management_backend_dotnet v1"));
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
