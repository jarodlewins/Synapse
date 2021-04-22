using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Synapse.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Synapse.Models;
using Synapse.Areas.Identity.Data;
using System.Collections.Generic;
//THIS FILE IS FOR DEVELOPMENT PURPOSES ONLY
namespace Synapse.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new SynapseContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<SynapseContext>>()))
            {
                var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@brainstorm.com");
                await EnsureRole(serviceProvider, adminID, Constants.Class_EventAdministratorsRole);

                // allowed user can create and edit contacts that they create
                var managerID = await EnsureUser(serviceProvider, testUserPw, "manager@brainstorm.com");
                await EnsureRole(serviceProvider, managerID, Constants.Class_EventManagersRole);

                SeedDB(context, adminID);
            }
        }
        public static void SeedDB(SynapseContext context, string adminID)
        {
            if (context.Class_Event.Any())
            {
                return;   // DB has been seeded
            }

            context.Class_Event.AddRange(
                new Class_Event
                {
                    StartTime = DateTime.Parse("2020-1-30 8:45:00"),
                    EndTime = DateTime.Parse("2020-1-30 10:00:00"),
                    NumStudents = 12,
                    NumInstructors = 1,
                    Organization = "IPSF",
                    Status = ClassStatus.Submitted,
                    Instructors = new List<Instructor>()
                },
                new Class_Event
                {
                    StartTime = DateTime.Parse("2021-6-23 16:45:00"),
                    EndTime = DateTime.Parse("2021-6-23 18:00:00"),
                    NumStudents = 25,
                    NumInstructors = 2,
                    Organization = "IPSF",
                    Status = ClassStatus.Approved,
                    Instructors = new List<Instructor>()
                }
            ) ;
            context.SaveChanges();
        }
        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                            string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<SynapseUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new SynapseUser
                {
                    UserName = UserName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<SynapseUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
    }
}