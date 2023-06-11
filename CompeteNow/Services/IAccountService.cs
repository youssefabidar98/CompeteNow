namespace CompeteNow.Services
{
    public interface IAccountService
    {
        Task LoginAsync(string email, string password, bool rememberMe);
        Task SigninAsync(string email, string password, DateTime birthday, string genre);
        Task LogoutAsync();
    }
}
