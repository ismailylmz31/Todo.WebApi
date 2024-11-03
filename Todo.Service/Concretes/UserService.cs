using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Exceptions;
using Todo.Models.Entities;
using Todo.Models.Users;
using Todo.Service.Abstract;

namespace Todo.Service.Concretes
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> ChangePasswordAsync(string id, ChangePasswordRequestDto dto)
        {
            var user = await _userManager.FindByIdAsync(id);
            UserIsPresent(user);

            var result = await _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);

            if (result.Succeeded is false)
            {
                throw new BusinessException(result.Errors.First().Description);
            }

            return "Şifre Değiştirildi.";
        }

        public async Task<User> CreateUserAsync(RegisterRequestDto registerRequestDto)
        {
            User user = new User()
            {
                Email = registerRequestDto.Email,
                UserName = registerRequestDto.Username,
                BirthDate = registerRequestDto.BirthDate,
            };



            var result = await _userManager.CreateAsync(user, registerRequestDto.Password);

            var role = await _userManager.AddToRoleAsync(user, "User");
            if (!role.Succeeded)
            {
                throw new BusinessException(role.Errors.First().Description);
            }


            return user;
        }

        public async Task<string> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            UserIsPresent(user);

            await _userManager.DeleteAsync(user);

            return "Kullanıcı Silindi.";
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            UserIsPresent(user);

            return user;
        }

        public async Task<User> LoginAsync(LoginRequestDto dto)
        {
            var userExist = await _userManager.FindByEmailAsync(dto.Email);
            UserIsPresent(userExist);

            var result = await _userManager.CheckPasswordAsync(userExist, dto.Password);

            if (result is false)
            {
                throw new NotFoundException("Parolanız yanlış.");
            }

            return userExist;
        }

        public async Task<User> UpdateAsync(string id, UpdateRequestDto dto)
        {
            var user = await _userManager.FindByIdAsync(id);
            UserIsPresent(user);

            user.UserName = dto.Username;
            user.BirthDate = dto.BirthDate;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded is false)
            {
                throw new BusinessException(result.Errors.First().Description);
            }

            return user;

        }

        private void UserIsPresent(User? user)
        {
            if (user is null)
            {
                throw new NotFoundException("Kullanıcı bulunamadı.");
            }
        }
    }
}
