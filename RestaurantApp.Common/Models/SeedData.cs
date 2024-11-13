﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Common.Models
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            if (!await roleManager.RoleExistsAsync("Admin")) //admin diye bir rol yok ise
            {
                await roleManager.CreateAsync(new IdentityRole("Admin")); //admin rolü ekle
            }
            if (!await roleManager.RoleExistsAsync("User")) //user diye bir rol yok ise
            {
                await roleManager.CreateAsync(new IdentityRole("User")); //user rolü ekle
            }

            //Admin kullanıcısı
            string adminEmail = "admin@domain.com";
            string adminPassword = "Admin123!";
            if(await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            //User kullanıcısı
            string userEmail = "user@domain.com";
            string userPassword = "User123!";
            if (await userManager.FindByEmailAsync(userEmail) ==null)
            {
                var normalUser = new ApplicationUser
                {
                    UserName = userEmail,
                    Email = userEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(normalUser, userPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(normalUser, "User");
                }
            }









        }
    }
}