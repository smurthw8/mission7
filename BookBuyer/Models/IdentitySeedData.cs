using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookBuyer.Models
{
    public static class IdentitySeedData
    {
        //cosnt = can't be changed later in the program
        private const string adminUser = "Admin";
        private const string adminPassword = "413ExtraYeetPeriod(t)!";

        //make sure there is data in database to start with, appbuilder > settings for congifure
        public static async void EnsurePopulated (IApplicationBuilder app)
        {
            //creates instance of context file, and changing/settings using appBuilder
            AppIdentityDBContext context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<AppIdentityDBContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            //set up identity manager
            UserManager<IdentityUser> userManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            //set up user, if comes back as null, create user in table
            IdentityUser user = await userManager.FindByIdAsync(adminUser);

            if(user == null)
            {
                user = new IdentityUser(adminUser);
                user.Email = "admin@dead.com";
                user.PhoneNumber = "111-2222";

                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}
