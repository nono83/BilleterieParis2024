using BilleterieParis2024.Data;
using BilleterieParis2024.Models;
using BilleterieParis2024.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace BilleterieParis2024
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            //builder.Services.AddDefaultIdentity<ApplicationUser>(options =>  options.SignIn.RequireConfirmedAccount = true)
            //    .AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => 
            {
                options.SignIn.RequireConfirmedAccount = true;

                // Paramètres utilisateur.
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";

                // Paramètres des mots de passe.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 9;

                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
           

            builder.Services.AddControllersWithViews();

            //Globalization et localization pour le multilanguages
            //builder.Services.AddControllersWithViews()
            //    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

            //builder.Services.Configure<RequestLocalizationOptions>(options =>
            //{
            //    var supportedCultures = new[]
            //    {
            //        //new CultureInfo("en-US"),
            //        //new CultureInfo("es"),
            //        new CultureInfo("fr")
            //    };
            //    options.DefaultRequestCulture = new RequestCulture(culture: "fr-FR", uiCulture: "fr");
            //    options.SupportedCultures = supportedCultures;
            //    options.SupportedUICultures = supportedCultures;
            //});


            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

            //Configuration pour SendGrid
            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

            //Configuration pour MailJet
            //builder.Services.AddTransient<IEmailSender, MailjetService>();

            //Gestion des sessions
            builder.Services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                //options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
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

            //app.MapControllerRoute(
            //    name: "areas",
            //    pattern: "{area=exists}/{controller}/{action}/{id?}");

            //app.MapControllerRoute(
            //name: "default",
            //pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
            name: "areas",
            pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

            app.UseSession();

            app.MapRazorPages();

            app.UseCookiePolicy();
            //Globalization et localization pour le multilanguages
            //var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            //app.UseRequestLocalization(locOptions.Value);

            app.Run();
        }
    }
}