using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace WebApplication_Dianthus.Models.Interface
{
    public interface IRoleRepository
    {
        void Create(Role instance);
 
        void Update(Role instance);
 
        void Delete(Role instance);
 
        Role GetRoleByID(int RoleID);
 
        IQueryable<Role> GetAll();
 
        void SaveChanges();
 
    }
}