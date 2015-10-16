namespace Winter.Auth.Service.Models
{
    public class LoginAttempt
    {
        public LoginAttempt(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }
    }
}