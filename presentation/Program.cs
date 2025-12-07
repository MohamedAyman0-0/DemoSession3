using DemoSession3.DataAccess.Data.Contexts;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;

using DemoSession3.DataAccess.Repositories.Departments;
using DemoSession3.BuisnessLogic.Services.Interfaces;
using DemoSession3.BuisnessLogic.Services.Classes;
using DemoSession3.BuisnessLogic.Profiles;
using DemoSession3.DataAccess.Repositories.Employees;
using Microsoft.AspNetCore.Mvc;

namespace Session3Demo.Presentation

{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region  Add services to the container.
            // Add services to the container.
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            // builder.Services.AddScoped <ApplicationDbContext>();
            //builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //{
            //    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
            //    options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            //    options.UseLazyLoadingProxies();

            //    });
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
            #endregion

            var app = builder.Build();

            #region Configure the HTTP request pipeline.

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            #endregion

            app.Run();
        }
    
    }  
}
