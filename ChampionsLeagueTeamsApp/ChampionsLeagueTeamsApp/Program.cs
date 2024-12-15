using Microsoft.EntityFrameworkCore;
using ChampionsLeagueTeamsApp.Data;
using Microsoft.AspNetCore.Identity;
using ChampionsLeagueTeamsApp.BusinessLogic;
using ChampionsLeagueTeamsApp.Hubs;

namespace ChampionsLeagueTeamsApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure services
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Register custom services
            builder.Services.AddScoped<ITitlesService, TitlesService>();
            builder.Services.AddScoped<ITeamService, TeamService>();
            builder.Services.AddScoped<ICoachService, CoachService>();
            builder.Services.AddScoped<IPlayerService, PlayerService>();
            builder.Services.AddScoped<IStadiumService, StadiumService>();

            // Configure SignalR
            builder.Services.AddSignalR();

            // MVC and Razor Pages settings
            builder.Services.AddControllersWithViews(options =>
            {
                options.SuppressOutputFormatterBuffering = true;
            }).AddRazorRuntimeCompilation();
            builder.Services.AddRazorPages();

            // Data protection
            builder.Services.AddDataProtection();

            var app = builder.Build();

            // Initialize database and roles
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                DbInitializer.Initialize(context);

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                // Ensure roles exist
                if (!await roleManager.RoleExistsAsync("Administrator"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Administrator"));
                }

                if (!await roleManager.RoleExistsAsync("User"))
                {
                    await roleManager.CreateAsync(new IdentityRole("User"));
                }

                // Create default admin user
                var adminEmail = "admin@example.com";
                var adminPassword = "Admin@123";
                if (await userManager.FindByEmailAsync(adminEmail) == null)
                {
                    var adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                    await userManager.CreateAsync(adminUser, adminPassword);
                    await userManager.AddToRoleAsync(adminUser, "Administrator");
                }
            }

            // Configure HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
                app.UseExceptionHandler("/Error/500");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            // SignalR Hub mapping
            app.MapHub<NotificationHub>("/notificationHub");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Add authentication and authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            // Admin area route
            app.MapControllerRoute(
                name: "admin",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

            // Default route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "identity",
                pattern: "Identity/{controller}/{action}/{id?}",
                defaults: new { area = "Identity" }
);

            // Razor Pages route
            app.MapRazorPages();

            app.Run();
        }
    }
}