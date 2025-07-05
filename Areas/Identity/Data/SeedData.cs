namespace CertiNet1.Data;

using CertiNet1.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var userManager = serviceProvider.GetRequiredService<UserManager<UserModel>>();

        string[] roleNames = { "Admin", "AgenteDeRegistro" };
        IdentityResult roleResult;

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        var adminEmail = "admin@certinet.com";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new UserModel()
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                Nome = "Admin Pedro Liu" 
            };

            var result = await userManager.CreateAsync(adminUser, "SenhaForte@123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}