using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepairWeb.Authorization;
using RepairWeb.Data;
using RepairWeb.Data.Services;

namespace RepairWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddScoped<RequestService>();
            builder.Services.AddScoped<ExecutorRequestService>();
            builder.Services.AddScoped<ReportService>();
            builder.Services.AddScoped<NotificationService>();
            

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddRazorPages();
            builder.Services.Configure<IdentityOptions>(options => options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ");

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.IsClient,
                    policyBuilder => policyBuilder.RequireClaim(Claims.UserRole, "клиент"));
                options.AddPolicy(Policies.IsExecutor,
                    policyBuilder => policyBuilder.RequireClaim(Claims.UserRole, "исполнитель"));
                options.AddPolicy(Policies.IsAdmin, policyBuilder => policyBuilder.RequireClaim(
                    Claims.UserRole, "admin"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.Run();
        }
    }
}
