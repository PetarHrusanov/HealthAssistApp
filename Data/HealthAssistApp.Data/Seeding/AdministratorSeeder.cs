﻿// <copyright file="AdministratorSeeder.cs" company="HealthAssistApp">
// Copyright (c) HealthAssistApp. All Rights Reserved.
// </copyright>

namespace HealthAssistApp.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using HealthAssistApp.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class AdministratorSeeder : ISeeder
    {
        public AdministratorSeeder()
        {
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            string email = "Administrator@abv.bg";
            string roleName = "Administrator";
            string password = "AdminPassword";

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
            };

            await userManager.CreateAsync(user, password);

            await userManager.AddToRoleAsync(user, roleName);

            await dbContext.SaveChangesAsync();
        }
    }
}
