
using NosiYa.Web.Infrastructure.ModelBinders;

namespace NosiYa.Web
{
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;
    using NosiYa.Services.Data.Interfaces;
    using Infrastructure.Extensions;


    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<NosiYaDbContext>(options =>
                options.UseSqlServer(connectionString));

            //builder.Services.AddDatabaseDeveloperPageExceptionFilter(); da se mahne TODO 

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
                    {
                        options.SignIn.RequireConfirmedAccount = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
                        options.Password.RequireDigit = builder.Configuration.GetValue<bool>("Identity:Password:RequireDigit");
                        options.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
                        options.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
                        options.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
                        options.Password.RequiredLength = builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
                        options.Password.RequiredUniqueChars = builder.Configuration.GetValue<int>("Identity:Password:RequiredUniqueChars");
                    }
                )
                .AddEntityFrameworkStores<NosiYaDbContext>();

            builder.Services.AddApplicationServices(typeof(IOutfitSetService));

            builder.Services.AddControllersWithViews()
                .AddMvcOptions(options =>
                {
                    //Should be inserted at the beginning, otherwise the default provider will handle the binding.
                    options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider()); 
                }); ;

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // This will remember is the user is using http or https and remember that for given time
                // The default HSTS value is 30 days.
                // You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts. 
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
            app.MapRazorPages();

            app.Run();
        }
    }
}