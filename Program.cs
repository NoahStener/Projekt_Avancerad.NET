
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Projekt_Avancerad.NET.Authentication;
using Projekt_Avancerad.NET.Data;
using Projekt_Avancerad.NET.Helper;
using Projekt_Avancerad.NET.Interfaces;
using Projekt_Avancerad.NET.Repository;
using ProjektModels;
using System.Text;
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
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version  = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter into field the word 'Bearer' followed by a space and the hwt token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                    }
                });

            });

            //Configure Identity and Entity Framework
            builder.Services.AddDbContext<ProjektDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ProjektDbContext>()
                .AddDefaultTokenProviders();

            //Configure JWT Authentication
            var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
            var issuer = builder.Configuration["Jwt:Issuer"];
            var audience = builder.Configuration["Jwt:Audience"];

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });


            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy =>
                    policy.RequireRole("admin"));
                options.AddPolicy("CompanyPolicy", policy =>
                    policy.RequireRole("company"));
                options.AddPolicy("CustomerPolicy", policy =>
                    policy.RequireRole("customer"));

                //Kombinerad policy för admin och company
                options.AddPolicy("AdminorCompanyPolicy", policy =>
                    policy.RequireAssertion(context =>
                        context.User.IsInRole("admin") || context.User.IsInRole("company")));

                //kombinerad policy för admin, company och customer
                options.AddPolicy("AdminCompanyOrCustomerPolicy", policy =>
                    policy.RequireAssertion(context =>
                        context.User.IsInRole("admin") || context.User.IsInRole("company") || context.User.IsInRole("customer")));
            });
           




            //Register application Services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICompanyCustomerRepository, CompanyCustomerRepository>();
            builder.Services.AddScoped<ICompanyAppointmentRepository, CompanyAppointmentRepository>();
            builder.Services.AddScoped<ICustomer, CustomerRepository>();
           
            

            //Minns inte om jag behövde denna
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.
                Serialization.ReferenceHandler.IgnoreCycles;
            });

            builder.Services.AddAutoMapper(typeof(MappingProfiles));

            var app = builder.Build();

            //seed roles and users
            using (var scope = app.Services.CreateScope())
            {
                var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                userService.SeedRolesAndUsersAync().Wait();
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthentication(); //Authentication middleware
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
