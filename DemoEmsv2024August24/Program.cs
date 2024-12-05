using DemoEmsv2024August24.Model;
using DemoEmsv2024August24.Repository;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace DemoEmsv2024August24
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            //3-json format
            builder.Services.AddControllersWithViews()
             .AddJsonOptions(
             options =>
             {
                 options.JsonSerializerOptions.PropertyNamingPolicy = null;
                 options.JsonSerializerOptions.ReferenceHandler = 
                 System .Text .Json .Serialization . ReferenceHandler.IgnoreCycles ; 
                 options .JsonSerializerOptions .DefaultIgnoreCondition =
                 System .Text .Json .Serialization .JsonIgnoreCondition .WhenWritingNull ;
                 options.JsonSerializerOptions.WriteIndented = true;
             });
            //connection string as Middleware

            builder.Services.AddDbContext<DemoAugust2024DbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("PropelAug24Connection")));

            //2- Register Repository and service layer

            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            var app = builder.Build();

           

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
