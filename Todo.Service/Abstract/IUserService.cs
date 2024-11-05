﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Entities;
using Todo.Models.Entities;
using Todo.Models.Users;

namespace Todo.Service.Abstract
{
    public interface IUserService
    {
        //Task<User> CreateUserAsync(RegisterRequestDto registerRequestDto);
        //Task<User> GetByEmailAsync(string email);

        Task<User> LoginAsync(LoginRequestDto dto);

        Task<string> DeleteAsync(string id);

        Task<User> UpdateAsync(string id, UpdateRequestDto dto);
        Task<string> ChangePasswordAsync(string id, ChangePasswordRequestDto dto);
        Task<ReturnModel<UserResponseDto>> CreateUserAsync(RegisterRequestDto dto);
        Task<ReturnModel<UserResponseDto>> GetUserByEmailAsync(string email);
        Task<ReturnModel<bool>> AddUserToRoleAsync(string email, string roleName);
        Task<ReturnModel<List<string>>> GetUserRolesAsync(string email);


    }
}
