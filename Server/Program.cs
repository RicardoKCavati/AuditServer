using AuditApp.Server.Controllers;
using AuditApp.Server.Database;
using AuditApp.Server.Database.Repositories;
using AuditApp.Server.Objects;
using AuditApp.Server.Services;
using AuditApp.Shared.Models.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace AuditApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
#if DEBUG
            var connString = "Server=localhost;User Id=root;Password=;Database=audit;DefaultCommandTimeout=60; Allow User Variables=true";
#else
            var connString = "Server=my-server231101.mariadb.database.azure.com;User Id=rcavati;Password=&rtnNDaS^69s%$;Database=audit;DefaultCommandTimeout=60; Allow User Variables=true";
#endif
            //todo ver como obter isso do appsettings.json
            builder.Services.AddDbContext<AuditContext>(options => options.UseMySql(connString, ServerVersion.AutoDetect(connString)));
            builder.Services.AddScoped<DbContext, AuditContext>();
            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
            builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
            builder.Services.AddScoped<JwtTokenHandler>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<EmailService>();
            builder.Services.AddScoped<AnswerService>();
            builder.Services.AddScoped<QuestionService>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();


            app.MapRazorPages();
            app.MapControllers();
            app.UseCors();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}