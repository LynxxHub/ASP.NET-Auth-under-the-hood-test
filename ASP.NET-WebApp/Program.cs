using ASP.NET_Auth_under_the_hood_test.Authorization.Handlers;
using ASP.NET_Auth_under_the_hood_test.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ASP.NET_Auth_under_the_hood_test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddAuthentication("LynxCookie")
                .AddCookie("LynxCookie", options =>
                {
                    options.Cookie.Name = "LynxCookie";
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", p => p.RequireClaim("Admin"));

                options.AddPolicy("CEO", p => p
                    .RequireClaim(ClaimTypes.Name, "Lynx").RequireClaim("Admin"));

                options.AddPolicy("18YearsOnly", p => p
                    .RequireClaim("Age")
                    .Requirements.Add(new UserAgeRequirement(18)));
            });

            builder.Services.AddSingleton<IAuthorizationHandler, UserAgeRequirementHandler>();

            builder.Services.AddHttpClient("JWTApi", c =>
            {
                c.BaseAddress = new Uri("https://localhost:7146/");
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}