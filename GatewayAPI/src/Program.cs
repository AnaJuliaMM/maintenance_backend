using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Config CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowMultipleOrigins",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:3000", "http://host.docker.internal:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});

// Config Authentication
builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
        "Bearer",
        options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "emissor",
                ValidAudience = "destinatario",
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("CLyuqWAaoBZIFSzkfworDetPz4YinvqG")
                ),
            };
        }
    );

// Add Ocelot e Swagger
builder.Services.AddOcelot();
builder.Services.AddSwaggerForOcelot(builder.Configuration);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Swagger API Gateway
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "API Gateway",
            Version = "v1",
            Description = "API Gateway para rotear requisições para as APIs downstream",
        }
    );

    // Configurações adicionais podem ser adicionadas aqui, como esquemas de segurança, se necessário.
    c.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Insira o token JWT",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
        }
    );
    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",
                    },
                },
                new string[] { }
            },
        }
    );
});

var app = builder.Build();

// Enable http requests
app.UseCors("AllowMultipleOrigins");

// Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gateway");
    c.SwaggerEndpoint("http://host.docker.internal:3011/swagger/v1/swagger.json", "User API");
    c.SwaggerEndpoint("http://host.docker.internal:3012/swagger/v1/swagger.json", "Machine API");
    c.SwaggerEndpoint("http://host.docker.internal:3013/swagger/v1/swagger.json", "Warehouse API");
    c.RoutePrefix = string.Empty;
});

// SwaggerForOcelot UI
app.UseSwaggerForOcelotUI(options =>
{
    options.PathToSwaggerGenerator = "/swagger/docs";
});

// Configurate app to use ocelot, that is the middleware that redirect
//      the gateway requisition to the correspondent microsservice
await app.UseOcelot();

app.Run();
