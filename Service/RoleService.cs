using WebApplication_Dianthus.Models.Interface;

namespace WebApplication_Dianthus.Models.Service;
public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepo;
    public RoleService(IRoleRepository roleRepo) => _roleRepo = roleRepo;

    public IEnumerable<Role> GetAllRoles() => _roleRepo.GetAll();
    public Role GetRole(int id) => _roleRepo.GetById(id);
    public void CreateRole(Role role) => _roleRepo.Add(role);
    public void UpdateRole(Role role) => _roleRepo.Update(role);
    public void DeleteRole(int id) => _roleRepo.Delete(id);
}