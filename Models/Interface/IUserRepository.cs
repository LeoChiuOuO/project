using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace WebApplication_Dianthus.Models.Interface
{
    public interface IUserRepository
    {
        void Create(User instance);
 
        void Update(User instance);
 
        void Delete(User instance);
 
        User GetUserByID(int UserID);
 
        IQueryable<User> GetAll();
 
        void SaveChanges();

    }
}