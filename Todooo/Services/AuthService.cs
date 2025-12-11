namespace Todooo.Services
{
    public interface IAuthService
    {
        bool Login(string email, string password);
    }

    public class AuthService : IAuthService
    {
        public bool Login(string email, string password)
        {
            return LoginService.Reverse(email) == password;
        }
    }
}
