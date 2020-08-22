using Autofac.Extensions.DependencyInjection;
using ITAcademy.TaskTwo.Data;
using ITAcademy.TaskTwo.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.Threading.Tasks;

namespace ITAcademy.TaskTwo.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File("SchoolManagement.log", outputTemplate: 
                "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] ({UserName}) {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    await AdminInitializer.InitializeAsync(userManager, rolesManager);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "An error occurred while seeding the database.");
                }
                host.Run();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
            .UseSerilog();         
    }
}