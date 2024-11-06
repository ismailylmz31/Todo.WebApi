using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Core.Entities;
using Todo.Service.Abstract;
using Todo.Core.Exceptions;
using Todo.Models.Entities;

namespace Todo.Service.Concretes
{
    public class RolesService : IRolesService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public RolesService(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<ReturnModel<string>> AddRoleAsync(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return new ReturnModel<string>
                {
                    Success = false,
                    Status = 400,
                    Message = "Rol ismi geçersiz."
                };
            }

            if (await _roleManager.RoleExistsAsync(roleName))
            {
                return new ReturnModel<string>
                {
                    Success = false,
                    Status = 400,
                    Message = "Bu rol zaten mevcut."
                };
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (result.Succeeded)
            {
                return new ReturnModel<string>
                {
                    Success = true,
                    Status = 200,
                    Data = roleName,
                    Message = "Rol başarıyla eklendi."
                };
            }

            throw new BusinessException("Rol eklenirken bir hata oluştu.");
        }

        public async Task<ReturnModel<string>> AssignRoleToUserAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException("Kullanıcı bulunamadı.");
            }

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                throw new NotFoundException("Rol bulunamadı.");
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return new ReturnModel<string>
                {
                    Success = true,
                    Status = 200,
                    Data = roleName,
                    Message = "Rol kullanıcıya başarıyla eklendi."
                };
            }

            throw new BusinessException("Rol ekleme işlemi başarısız oldu.");
        }

        public ReturnModel<List<string>> ListRoles()
        {
            var roles = _roleManager.Roles.Select(r => r.Name).ToList();
            return new ReturnModel<List<string>>
            {
                Success = true,
                Status = 200,
                Data = roles,
                Message = "Roller listelendi."
            };
        }
    }
}
