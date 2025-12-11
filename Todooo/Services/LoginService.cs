namespace Todooo.Services
{
    public static class LoginService
    {
        public static string Reverse(string input)
        {
            var arr = input.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

    }
}


