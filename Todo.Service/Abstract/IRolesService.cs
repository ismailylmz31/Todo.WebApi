using System.Threading.Tasks;
using Todo.Core.Entities;

namespace Todo.Service.Abstract
{
    public interface IRolesService
    {
        Task<ReturnModel<string>> AddRoleAsync(string roleName);
        Task<ReturnModel<string>> AssignRoleToUserAsync(string userId, string roleName);
        ReturnModel<List<string>> ListRoles();
    }
}
