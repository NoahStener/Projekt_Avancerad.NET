
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Projekt_Avancerad.NET.Data;
using Projekt_Avancerad.NET.Helper;
using Projekt_Avancerad.NET.Interfaces;
using Projekt_Avancerad.NET.Repository;
using ProjektModels;
using System.Text.Json.Serialization;

namespace Projekt_Avancerad.NET
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
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(MappingProfiles));

            builder.Services.AddScoped<ICompanyCustomerRepository, CompanyCustomerRepository>();
            builder.Services.AddScoped<ICompanyAppointmentRepository, CompanyAppointmentRepository>();
            builder.Services.AddScoped<ICustomerAppointmentRepository, CustomerAppointmentRepository>();
           

            //Minns inte om jag behövde denna
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.
                Serialization.ReferenceHandler.IgnoreCycles;
            });

            //JsonConvert.SerializeObject(, Formatting.Indented, new JsonSerializerSettings
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            //});


            //builder.Services.AddControllers().AddJsonOptions(options =>
            //{
            //  options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            //});

            builder.Services.AddDbContext<ProjektDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

            
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
