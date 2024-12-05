using DemoEmsv2024August24.Model;
using Microsoft.EntityFrameworkCore;
using project_1.Repository;
using System.Text.Json.Serialization;

namespace project_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //3-json format
            builder.Services.AddControllersWithViews()
             .AddJsonOptions(
             options =>
             {
                 options.JsonSerializerOptions.PropertyNamingPolicy = null;
                 options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                 options.JsonSerializerOptions.WriteIndented = true;
             });
            //connection string as Middleware

            builder.Services.AddDbContext<CustomerAssignmentContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("assignment1")));

            //2- Register Repository and service layer

            builder.Services.AddScoped<IOrderRepository, OrderRepository>();


            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
