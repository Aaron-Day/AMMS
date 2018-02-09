using AMMS.Models;
using AMMS.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AMMS.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            if (context.Roles.Count() < 5)
            {
                InitializeRoles(context);
            }
            if (!context.Users.Any())
            {
                InitializeUsers(context);
            }
        }

        public static void InitializeRoles(ApplicationDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roles = new List<string> { "Admin", "PC", "QC", "TI", "CE" };

            foreach (var role in roles)
            {
                if (context.Roles.Any(r => r.Name == role)) continue;

                var idRole = new IdentityRole()
                {
                    Name = role,
                    NormalizedName = role.ToUpper()
                };
                var result = roleStore.CreateAsync(idRole).Result;
            }
            context.SaveChanges();
        }

        public static void InitializeUsers(ApplicationDbContext context)
        {
            const string email = "admin@us.army.mil";

            var user = new ApplicationUser()
            {
                FirstName = "Admin",
                LastName = "User",
                Email = email.ToLower(),
                NormalizedEmail = email.ToUpper(),
                PhoneNumber = "+19717067846",
                PhoneNumberConfirmed = true,
                SocialSecurityNumber = "000000000",
                DateOfBirth = new DateTime(2001, 11, 01),
                UserName = email.ToLower(),
                NormalizedUserName = email.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D"),
                FullName = "User, Admin",
                AssignedUnit = "ADMIN"
            };

            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(user, PasswordProtocol.CalculateHash("Secret123$", user.Salt));
            user.PasswordHash = hashed;

            var userManager = context.GetService<UserManager<ApplicationUser>>();
            var userStore = new UserStore<ApplicationUser>(context);
            var result = userStore.CreateAsync(user).Result;
            result = userManager.AddToRoleAsync(user, "ADMIN").Result;

            context.SaveChanges();
        }
    }
}
