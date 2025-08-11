using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication_Dianthus.Models.Interface;

namespace WebApplication_Dianthus.Models
{
    public class UserRepository : IUserRepository
    {
        public void Create(User instance)
        {
            throw new NotImplementedException();
        }

        public void Delete(User instance)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetUserByID(int UserID)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(User instance)
        {
            throw new NotImplementedException();
        }
    }
}