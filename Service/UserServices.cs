using Microsoft.AspNetCore.Http.HttpResults;
using WebApplication_Dianthus.Models.Interface;
using WebApplication_Dianthus.Models.Service.Interface;

namespace WebApplication_Dianthus.Models
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IOperationLogService _log;
        public UserService(IUserRepository userRepo, IOperationLogService log)
        {
            _userRepo = userRepo;
            _log = log;
        }

        public IEnumerable<User> GetAllUsers() => _userRepo.GetAll();
        public User GetByAccout(string account) => _userRepo.GetByAccount(account);
        public User GetById(int id) => _userRepo.GetById(id);
        public void CreateUser(User user)
        {
            var existing = _userRepo.GetByAccount(user.Account);
            var success = true;
            if (existing != null){
                success = false;
                _log.Log(actionType: "CreateUser",module: "UserService", success: success, description: "帳號已存在，請使用其他帳號");
                throw new ArgumentException("帳號已存在，請使用其他帳號");
            }
            if (string.IsNullOrWhiteSpace(user.Password)){
                throw new ArgumentException("密碼為必填");
            }

            if (!IsPasswordValid(user.Password)){
                throw new ArgumentException("密碼必須至少6位，且包含大小寫字母與數字");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _log.Log(actionType: "CreateUser",module: "UserService", success: success, description: "帳號建立成功");
            _userRepo.Add(user);

        }

        private bool IsPasswordValid(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;
            if (password.Length < 6) return false;
            if (!password.Any(char.IsUpper)) return false; // 至少一個大寫
            if (!password.Any(char.IsLower)) return false; // 至少一個小寫
            if (!password.Any(char.IsDigit)) return false; // 至少一個數字
            return true;
        }


        public void UpdateUser(User user)
        {
            var existingUser = _userRepo.GetById(user.Id);
            existingUser.Email = user.Email;
            if (existingUser == null) return;
            if (user.Name != null) {
                existingUser.Name = user.Name;
            } 

            // 如果密碼有輸入新值才重新雜湊
            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                if (!IsPasswordValid(user.Password))
                {
                    throw new ArgumentException("密碼必須至少6位，且包含大小寫字母與數字");
                }
                existingUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            }
            else
            {
                // 沒輸入密碼就保留原本的
                user.Password = existingUser.Password;
            }


            _userRepo.Update(existingUser);
        }

        public void DeleteUser(int id) => _userRepo.Delete(id);
    }
}