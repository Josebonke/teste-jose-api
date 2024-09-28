using Microsoft.AspNetCore.Identity;

namespace teste_jose_api.identity
{
    public class AppUsuario : IdentityUser
    {
        public int TentativasDeLogin { get; set; } = 0;
    }
}
