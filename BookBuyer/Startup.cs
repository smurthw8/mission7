using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookBuyer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookBuyer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<BookstoreContext>(options =>
           {
               options.UseSqlite(Configuration["ConnectionStrings:BooksDBConnection"]);
           });

            services.AddScoped<IBookBuyerRepository, EFBookBuyerRepository>();

            services.AddRazorPages();

            //enable sessions
            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                //if pass in categorty and page num
                endpoints.MapControllerRoute("categorypage", "{bookCategory}/page{pageNum}",
                    new { Controller = "Home", action = "Index" });


                //add new endpoint passing in pagenum:
                endpoints.MapControllerRoute(
                    name: "Paging",
                    //what being passed into url, what you want to see,
                    //brakets = dynamic
                    pattern: "page{pageNum}",
                    //then do this
                    defaults: new {Controller = "Home", action = "Index"}
                    );

                //if just get category > sets defult page (doesn't pass in page itself if jsut sepcifiying category)
                endpoints.MapControllerRoute("category", "{bookCategory}",
                    new { Controller = "Home", action = "Index", pageNum = 1 });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
