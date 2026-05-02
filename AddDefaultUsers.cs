using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Aromo.Models;
using Aromo;

namespace Aromo
{
    public class AddDefaultUsers
    {
        public const string ADMIN_ROLE = "SuperUser";
        public const string USER_ROLE = "User";

        private const string ADMIN_EMAIL = "admin@aromo.local";
        private const string ADMIN_PASSWORD = "Admin!234";

        private const string DEMO_EMAIL = "user@aromo.local";
        private const string DEMO_PASSWORD = "User!234";

        private const string ADMIN_FIRSTNAME = "Admin";
        private const string ADMIN_LASTNAME = "Administrator";
        private const string ADMIN_PHONE = "+359888000111";

        private const string USER_FIRSTNAME = "Customer";
        private const string USER_LASTNAME = "Customer";
        private const string USER_PHONE = "+359888999222";

        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;

        public AddDefaultUsers(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.context = context;
        }

        public async Task SeedAsync()
        {
            await CreateRolesIfMissingAsync();
            await CreateInitialUsersAsync();
        }

        private async Task CreateRolesIfMissingAsync()
        {
            await CreateRoleAsync(ADMIN_ROLE);
            await CreateRoleAsync(USER_ROLE);
        }

        private async Task CreateRoleAsync(string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        private async Task CreateInitialUsersAsync()
        {
            await EnsureUserAsync(
                ADMIN_EMAIL,
                ADMIN_PASSWORD,
                ADMIN_ROLE,
                ADMIN_FIRSTNAME,
                ADMIN_LASTNAME,
                ADMIN_PHONE);

            await EnsureUserAsync(
                DEMO_EMAIL,
                DEMO_PASSWORD,
                USER_ROLE,
                USER_FIRSTNAME,
                USER_LASTNAME,
                USER_PHONE);
        }

        private async Task EnsureUserAsync(
            string email,
            string password,
            string role,
            string firstName,
            string lastName,
            string phone)
        {
            var existingUser = await userManager.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (existingUser != null)
                return;

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phone,
            };

            var createResult = await userManager.CreateAsync(user, password);

            if (!createResult.Succeeded)
                return;

            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }

            await userManager.AddToRoleAsync(user, role);
        }
    }
}
