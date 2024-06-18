namespace WebApplication.Models
{
    public class TokenResponse
    {
        public string access_token { get; set; }
        public string username { get; set; }    

        public TokenResponse(string access_token, string username)
        {
            this.access_token = access_token;   
            this.username = username;
        }
    }
}
