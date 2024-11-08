using Microsoft.EntityFrameworkCore;
using MachineAPI.API.Extensions;
using MachineAPI.Infrastructure.Data;
using MachineAPI.Domain.Interfaces;
using Microsoft.OpenApi.Models;
using MachineAPI.Application.Services;
using MachineAPI.Application.Interfaces;
using System.Text;
using AutoMapper;
public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(
            options =>options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"))
        );

        services.AddScoped<IMachineRepository, MachineRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();

        services.AddScoped<IMachineService, MachineService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ILocationService, LocationService>();

        services.AddAutoMapper(typeof(MappingDTO));

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { 
                Title = "Machine Authentication API", 
                Version = "v1" });
        });  

        services.AddAuthentication();
        services.AddAuthorization();

        services.AddSingleton<IConfiguration>(_configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {

        // Configure the HTTP request pipeline.
        if (env.IsDevelopment() || env.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Authentication API V1"); // Set up the Swagger UI
                c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
            });
            app.ApplyMigrations();
        }

        // app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
