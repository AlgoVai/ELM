namespace ELearningWeb.DTO
{
    public class AuthDTO
    {

        public string Token { get; set; }
        public bool Success { get; set; }
        public string RefreshToken { get; set; }
        public int expires_in { get; set; }
        public DateTime ActionTime { get; set;}

    }
}
