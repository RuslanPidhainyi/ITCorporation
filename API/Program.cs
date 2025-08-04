using API.Data;
using API.Interfaces;
using API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        //INFO: config Deserialization/Serialization
        builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
        });

        //INFO: config Swagger
        builder.Services.AddOpenApi();

        //INFO: config for bd
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        //INFO: config AutoMapper
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        //INFO: Dependency Injection
        builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
        builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        //INFO: config Cross-Origin Resource Sharing
        builder.Services.AddCors();

        var app = builder.Build();

        //INFO: 43 - 59 lines theare for the middleware pipeline
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "api");
            });
        }
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
        .WithOrigins("http://localhost:4200", "https://localhost:4200"));
        app.MapControllers();
        app.Run();
    }
}