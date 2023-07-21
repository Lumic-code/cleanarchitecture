using Employee.Application.Handlers;
using Employee.Core.Repositories.Base;
using Employee.Core.Repositories;
using Employee.Infrastructure.Data;
using Employee.Infrastructure.Repositories.Base;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<EmployeeContext>(m => m.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDB")), ServiceLifetime.Singleton);
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Employee.API",
                Version = "v1"
            });
        }

         );
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddMediatR(typeof(CreateEmployeeHandler).GetTypeInfo().Assembly);
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}