using System.Text;
using AutoMapper;
using MachineAPI.API.Extensions;
using MachineAPI.Application.Interfaces;
using MachineAPI.Application.Services;
using MachineAPI.Domain.Interfaces;
using MachineAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"))
        );

        services.AddScoped<IMachineRepository, MachineRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();

        services.AddScoped<IMachineService, MachineService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ILocationService, LocationService>();

        services.AddAutoMapper(typeof(MappingDTO));

        services.AddCors(options =>
        {
            options.AddPolicy(
                "AllowLocalhost3000",
                builder =>
                {
                    builder
                        .WithOrigins("http://localhost:3000", "http://host.docker.internal:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                }
            );
        });

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Machine API", Version = "v1" });
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

        // Habilitar CORS antes do roteamento
        app.UseCors("AllowLocalhost3000");

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
