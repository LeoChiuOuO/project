using WebApplication_Dianthus.Models;
using WebApplication_Dianthus.Models.Interface;
using WebApplication_Dianthus.Models.Service.Interface;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public bool ValidateUser(string account, string password, out User user)
    {
        user = _userRepository.GetByAccount(account);
        if (user == null) return false;
        return BCrypt.Net.BCrypt.Verify(password, user.Password);
    }

    public void UpdateLastLoginDate(User user)
    {
        user.LastLoginDate = DateTime.Now;
        _userRepository.Update(user);
    }
}