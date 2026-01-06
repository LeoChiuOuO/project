namespace WebApplication_Dianthus.Models.Interface;

public interface IUserRepository
{
    IEnumerable<User> GetAll();
    User GetByAccount(string account);
    User GetByAccountWithRelations(string account); // 新增：帶出關聯
    User GetById(int id);
    void Add(User user);
    void Update(User user);
    void Delete(int id);    
}