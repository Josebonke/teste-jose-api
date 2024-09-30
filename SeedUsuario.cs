using Microsoft.AspNetCore.Identity;
using teste_jose_api.identity;

namespace teste_jose_api
{
    public class SeedUsers
    {
        private readonly UserManager<AppUsuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUsers(UserManager<AppUsuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InsertUserAsync()
        {
            
            var existingUser = await _userManager.FindByEmailAsync("jose@teste.com");
            if (existingUser == null)
            {
                
                var user = new AppUsuario
                {
                    UserName = "JoseTeste",
                    Nome = "José Teste",
                    Email = "jose@teste.com",
                    EmailConfirmed = true,
                };
               
                var result = await _userManager.CreateAsync(user, "Jose@123");

                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync("Admin"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    }

                    await _userManager.AddToRoleAsync(user, "Admin");

                    
                    await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Permission", "CanManageUsers"));
                }
                else
                {
                   
                    foreach (var error in result.Errors)                    {
                        
                        Console.WriteLine(error.Description);
                    }
                }
            }
            else
            {
                Console.WriteLine("Usuário já existe.");
            }
        }
    }
}
