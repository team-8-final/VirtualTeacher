using Microsoft.EntityFrameworkCore;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Repositories;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Services;
using VirtualTeacher.Helpers;
using VirtualTeacher.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

namespace VirtualTeacher;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Language School API", Version = "v1" });

            //authentication for swagger below, adds the "Authenticate" button to the Swagger UI

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter JWT Token with Bearer format: bearer[space]token"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                    new string[]{}
                    }
                });
        });


        //Session
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(10);
            options.Cookie.HttpOnly = true;
            options.Cookie.Name = ".VirtualTeacher.Session";
            options.Cookie.IsEssential = true;
        });

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
        });

        //Repos
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        //Services
        builder.Services.AddScoped<IUserService, UserService>();

        //Helpers
        builder.Services.AddScoped<ModelMapper>();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }


        app.UseRouting();
        app.UseSession();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Language School API");
            options.RoutePrefix = "api/swagger";
        });


        app.UseAuthentication();
        app.UseAuthorization();
        app.UseStaticFiles();

        



        app.UseAuthorization();


        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}