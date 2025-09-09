using WebApplication_Dianthus.Models.Interface;
using WebApplication_Dianthus.Models.Service.Interface;

namespace WebApplication_Dianthus.Models
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        public UserService(IUserRepository userRepo) => _userRepo = userRepo;

        public IEnumerable<User> GetAllUsers() => _userRepo.GetAll();
        public User GetByAccout(string account) => _userRepo.GetByAccount(account);
        public User GetById(int id) => _userRepo.GetById(id);
        public void CreateUser(User user) => _userRepo.Add(user);
        public void UpdateUser(User user) => _userRepo.Update(user);
        public void DeleteUser(int id) => _userRepo.Delete(id);
    }
}