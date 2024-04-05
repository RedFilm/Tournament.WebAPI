using Microsoft.AspNetCore.Identity;
using Tournaments.Domain.Enums;

namespace Tournaments.Persistence.Initializers
{
	public static class DbInitializer
    {
        public static async Task InitializeRoles(RoleManager<IdentityRole<long>> roleManager)
        { 
            foreach (var role in Enum.GetNames(typeof(IdentityRoles)))
            {
                await CreateRoleIfNotExistsAsync(roleManager, role);
            }
        }

        private static async Task CreateRoleIfNotExistsAsync(RoleManager<IdentityRole<long>> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
                await roleManager.CreateAsync(new IdentityRole<long>(roleName));
        }
    }
}
