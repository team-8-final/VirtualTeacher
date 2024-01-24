using Microsoft.EntityFrameworkCore;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Repositories;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Services;
using VirtualTeacher.Helpers;

namespace VirtualTeacher;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

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

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "VirtualTeacher API");
            options.RoutePrefix = "api/swagger";
            // options.OAuthUseBasicAuthenticationWithAccessCodeGrant();
        }); 
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}