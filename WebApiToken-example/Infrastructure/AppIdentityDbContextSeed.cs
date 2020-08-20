using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ngServer.Infrastructure.Contexts;
using ngServer.Infrastructure.Security;

namespace ngServer.Infrastructure
{
    public class AppIdentityDbContextSeed
    {
        private readonly AppIdentityDbContext dbContext;

        public AppIdentityDbContextSeed(AppIdentityDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            dbContext.Database.EnsureCreated();

            await roleManager.CreateAsync(new IdentityRole("Administrators"));

            var defaultUser = new ApplicationUser { UserName = "juanjo.rm00@gmail.com", Email = "juanjo.rm00@gmail.com" };
            await userManager.CreateAsync(defaultUser, "Pass@word1");

            string adminUserName = "admin@localhost.com";
            var adminUser = new ApplicationUser { UserName = adminUserName, Email = adminUserName };
            await userManager.CreateAsync(adminUser, "Pass@word1");

            adminUser = await userManager.FindByNameAsync(adminUserName);
            await userManager.AddToRoleAsync(adminUser, "Administrators");

            dbContext.SaveChanges();
        }
    }
}
