namespace teste_jose_api.Model
{
    public class LogoutRequest
    {
        public int Id { get; set; }
        public string Token { get; set; } = "";
        public DateTime RevokedAt { get; set; } = DateTime.Now;
    }
}
