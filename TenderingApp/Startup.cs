using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TenderingApp.Data;

using TenderingApp.Services;

namespace TenderingApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            //services.AddRazorPages();
            services.AddRazorPages(options =>
                {
                    options.Conventions.AuthorizeAreaPage("Categories", "/Index");
                    options.Conventions.AuthorizeAreaPage("Categories", "/Create");
                    options.Conventions.AuthorizeAreaPage("Categories", "/Edit");
                    options.Conventions.AuthorizeAreaPage("Categories", "/Details");
                    options.Conventions.AuthorizeAreaPage("Categories", "/Delete");

                    options.Conventions.AuthorizeAreaPage("SubCategories", "/Index");
                    options.Conventions.AuthorizeAreaPage("SubCategories", "/Create");
                    options.Conventions.AuthorizeAreaPage("SubCategories", "/Edit");
                    options.Conventions.AuthorizeAreaPage("SubCategories", "/Details");
                    options.Conventions.AuthorizeAreaPage("SubCategories", "/Delete");
                    options.Conventions.AuthorizeAreaPage("SubCategories", "/GetSubCategory");

                    options.Conventions.AuthorizeAreaPage("Organizations", "/Index");
                    options.Conventions.AuthorizeAreaPage("Organizations", "/Create");
                    options.Conventions.AuthorizeAreaPage("Organizations", "/Edit");
                    //options.Conventions.AuthorizeAreaPage("Organizations", "/Details");
                    options.Conventions.AuthorizeAreaPage("Organizations", "/Delete");
                    
                    options.Conventions.AuthorizeAreaPage("Tenders", "/Create");
                    options.Conventions.AuthorizeAreaPage("Tenders", "/Edit");                    
                    options.Conventions.AuthorizeAreaPage("Tenders", "/Delete");

                });

            services.AddTransient<ISubCategoryService, SubCategoryService>();



            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
