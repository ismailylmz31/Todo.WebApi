using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Models.Entities;

namespace Todo.Repository.Repository.Abstract
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user, string password);
        Task<User> GetUserByEmailAsync(string email);
        Task<List<string>> GetUserRolesAsync(User user);
        Task<bool> AddUserToRoleAsync(User user, string roleName);
    }
}
