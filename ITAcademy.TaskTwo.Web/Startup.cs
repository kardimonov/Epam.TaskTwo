using Autofac;
using AutoMapper;
using FluentValidation.AspNetCore;
using ITAcademy.TaskTwo.Data.Context;
using ITAcademy.TaskTwo.Data.Interfaces;
using ITAcademy.TaskTwo.Data.Repositories;
using ITAcademy.TaskTwo.Logic.Middleware;
using ITAcademy.TaskTwo.Logic.Profiles;
using ITAcademy.TaskTwo.Logic.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using WebApiContrib.Core.Formatter.Csv;
using ITAcademy.TaskTwo.Logic.MessageHandlers;
using ITAcademy.TaskTwo.Logic.Interfaces;
using ITAcademy.TaskTwo.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace ITAcademy.TaskTwo.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IMessageFactory, MessageFactory>();

            services.AddAutoMapper(typeof(Startup), typeof(EmployeeDtoProfile),
                typeof(SubjectDtoProfile), typeof(PositionDtoProfile));

            services.AddDbContext<ApplicationContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);
            services.AddScoped<IApplicationContext, ApplicationContext>();

            services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 5;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            })
                .AddEntityFrameworkStores<ApplicationContext>();

            services.AddControllersWithViews();
            
            var csvFormatterOptions = new CsvFormatterOptions();
            services.AddControllers(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.InputFormatters.Add(new CsvInputFormatter(csvFormatterOptions));
                options.OutputFormatters.Add(new CsvOutputFormatter(csvFormatterOptions));
                options.FormatterMappings.SetMediaTypeMappingForFormat("csv", MediaTypeHeaderValue.Parse("text/csv"));
            })
                .AddXmlSerializerFormatters()
                .AddCsvSerializerFormatters();

            services.AddSignalR();

            services.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddSwaggerDocument();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<LogUserNameMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<SignalHub>("/notify");
                endpoints.MapControllers();
            });
        }
    }
}