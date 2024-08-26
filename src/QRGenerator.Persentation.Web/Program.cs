using Microsoft.AspNetCore.Authentication.Cookies;
using QRGenerator.DataAccess;
using QRGenerator.Persentation.Web.Configuration;
using QRGenerator.Persentation.Web.Services;

namespace QRGenerator.Persentation.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //dependensi injection
            IServiceCollection services = builder.Services;
            Depedencies.AddDataAccessServices(services, builder.Configuration);
            services.AddBusinessService();
            services.AddScoped<UserService>();
            services.AddScoped<QRGenerateService>();
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options =>
              {
                  options.LoginPath = "/Auth/Login";
                  options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                  options.AccessDeniedPath = "/Auth/AccesDanied";
              });

            var app = builder.Build();

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
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
