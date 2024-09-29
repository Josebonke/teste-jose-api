using Microsoft.AspNetCore.Identity;

namespace teste_jose_api.identity
{
    public class AppUsuario : IdentityUser
    {
        public string Nome { get; set; } = "";       
      
        public int TentativasDeLoginErradas { get; set; } = 0;
        public bool Bloqueado { get; set; } = false;
    }
}
