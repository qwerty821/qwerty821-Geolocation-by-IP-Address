using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System;
using WebApplication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using WebApplication.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
namespace WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


            builder.Services.AddControllersWithViews();
            
            builder.Services.AddSession();
          
            builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer      = true,
                    ValidIssuer         = AuthOptions.ISSUER,
                    ValidateAudience    = true,
                    ValidAudience       = AuthOptions.AUDIENCE,
                    ValidateLifetime    = true,
                    IssuerSigningKey    = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };
            });
            
            builder.Services.AddAuthorization();
            
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            builder.Services.AddTimeService();
            builder.Services.AddMongoDB();
            builder.Services.AddHttpClient();
            builder.Services.AddLocation();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

           
            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();
            
            app.MapControllers();


            app.Run();
        }
 
    }
}
