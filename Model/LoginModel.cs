namespace teste_jose_api.Model
{
    public class LoginModel
    {
        public class UsuarioFilter
        {
            public int Id { get; set; }
            public string Email { get; set; } = "";
            public string Senha { get; set; } = "";
           
        }


        public class UsuarioGrid
        {
            public int Id { get; set; }
            public string Nome { get; set; } = "";
            public string Email { get; set; } = "";
            public string Senha { get; set; } = "";
            public bool IsBloqueado { get; set; } = false;
            public int TentativasDeAcess0 { get; set; } = 0;
        }

        public class JwtToken
        {
            public int Id { get; set; }
            public string Token { get; set; } = "";
            public DateTime Expericao { get; set; } = new();

            public int UserId { get; set; }
            
        }
    }
}
