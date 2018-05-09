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
            if (!context.AircraftModels.Any())
            {
                InitializeAircraftModels(context);
            }
            if (!context.Units.Any())
            {
                InitializeUnits(context);
            }
            if (!context.Aircraft.Any())
            {
                InitializeAircraft(context);
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
                    NormalizedName = role?.ToUpper()
                };
                roleStore.CreateAsync(idRole).Wait();
            }
            context.SaveChanges();
        }

        public static void InitializeUsers(ApplicationDbContext context)
        {
            const string email = "admin@us.army.mil";

            var admin = new ApplicationUser()
            {
                FirstName = "Admin",
                LastName = "User",
                Email = email.ToLower(),
                NormalizedEmail = email?.ToUpper(),
                PhoneNumber = "+19717067846",
                PhoneNumberConfirmed = true,
                SocialSecurityNumber = "000000000",
                DateOfBirth = Formatting.AsMilDate(new DateTime(2001, 11, 01)),
                UserName = email.ToLower(),
                NormalizedUserName = email?.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D"),
                FullName = "User, Admin",
                AssignedUnit = "ADMIN"
            };

            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(admin, PasswordProtocol.CalculateHash("Secret123$", admin.Salt));
            admin.PasswordHash = hashed;

            var userManager = context.GetService<UserManager<ApplicationUser>>();
            userManager.CreateAsync(admin).Wait();
            userManager.AddToRoleAsync(admin, "ADMIN").Wait();

            context.SaveChanges();

            var qc = new ApplicationUser()
            {
                Rank = "SPC",
                FirstName = "Aaron",
                MiddleName = "Lloyd",
                LastName = "Day",
                Email = "aaron.l.day@us.army.mil",
                NormalizedEmail = "AARON.L.DAY@US.ARMY.MIL",
                PhoneNumber = "+19717067846",
                PhoneNumberConfirmed = true,
                SocialSecurityNumber = "000005327",
                DateOfBirth = Formatting.AsMilDate(new DateTime(1983, 07, 06)),
                UserName = "aaron.l.day@us.army.mil",
                NormalizedUserName = "AARON.L.DAY@US.ARMY.MIL",
                SecurityStamp = Guid.NewGuid().ToString("D"),
                FullName = "SPC Day, Aaron",
                AssignedUnit = "WFJ5B0"
            };

            hashed = password.HashPassword(qc, PasswordProtocol.CalculateHash("Secret123$", qc.Salt));
            qc.PasswordHash = hashed;

            userManager.CreateAsync(qc).Wait();
            userManager.AddToRoleAsync(qc, "QC").Wait();
            userManager.AddToRoleAsync(qc, "TI").Wait();
            userManager.AddToRoleAsync(qc, "CE").Wait();

            context.SaveChanges();
        }

        public static void InitializeAircraftModels(ApplicationDbContext context)
        {
            var models = new[] { "UH-60A", "UH-60L", "EH-60A", "HH-60A", "HH-60L" };
            var nsns = new[] { "1520010350266", "1520012984532", "1520010820686", "1520014599468", "1520014716743" };
            var eics = new[] { "RSA", "RSM", "RSB", "RSN", "RSI" };
            for (var i = 0; i < models.Length; ++i)
            {
                var model = new AircraftModel
                {
                    Id = models[i] + "ID",
                    Eic = eics[i],
                    Mds = models[i],
                    Nsn = nsns[i],
                    Name = "Blackhawk",
                    AllThisModelAircraft = new List<Aircraft>()
                };

                context.AircraftModels.Add(model);
                context.SaveChanges();
            }
        }

        public static void InitializeUnits(ApplicationDbContext context)
        {
            var units = new List<Unit>
            {
                new Unit
                {
                    Id = "WFJ5A0ID",
                    CompanyName = "Blue Stars",
                    UnitName = "Aco 3/158 AVN REGT",
                    Station = "GAAF",
                    UIC = "WFJ5A0",
                    UnitPhone = "9717067843"
                },
                new Unit
                {
                    Id = "WFJ5B0ID",
                    CompanyName = "Catfish",
                    UnitName = "Bco 3/158 AVN REGT",
                    Station = "GAAF",
                    UIC = "WFJ5B0",
                    UnitPhone = "9717067844"
                },
                new Unit
                {
                    Id = "WFJ5D0ID",
                    CompanyName = "Rebels",
                    UnitName = "Dco 3/158 AVN REGT",
                    Station = "GAAF",
                    UIC = "WFJ5D0",
                    UnitPhone = "9717067846"
                }
            };

            foreach (var unit in units)
            {
                context.Units.Add(unit);
                context.SaveChanges();
            }
        }

        public static void InitializeAircraft(ApplicationDbContext context)
        {
            var aircraft = new List<Aircraft>
            {
                new Aircraft
                {
                    SerialNumber = "9626674",
                    AcftHrs = 3452.8,
                    AircraftModelId = "UH-60LID",
                    UnitId = "WFJ5B0ID"
                },
                new Aircraft
                {
                    SerialNumber = "9626677",
                    AcftHrs = 4390.1,
                    AircraftModelId = "UH-60LID",
                    UnitId = "WFJ5B0ID"
                },
                new Aircraft
                {
                    SerialNumber = "9526682",
                    AcftHrs = 4882.0,
                    AircraftModelId = "UH-60LID",
                    UnitId = "WFJ5A0ID"
                }
            };
        }
    }
}
