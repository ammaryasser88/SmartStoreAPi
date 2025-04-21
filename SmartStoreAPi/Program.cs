
using Microsoft.EntityFrameworkCore;
using SmartStore.Application.Validators;
using SmartStore.Domain.Context;
using SmartStore.Extensions;

namespace SmartStoreAPi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            builder.Services.RegisterServices(configuration);

            var connString = builder.Configuration.GetConnectionString("SqlServerConnectionString");
            builder.Services.AddDbContext<SmartStoreContext>(options => options.UseSqlServer(connString));

            // Add services to the container.

            builder.Services.AddControllers();

            


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
}
