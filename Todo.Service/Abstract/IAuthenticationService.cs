using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models.Tokens;
using Todo.Models.Users;

namespace Todo.Service.Abstract
{
    public interface IAuthenticationService
    {

        Task<TokenResponseDto> RegisterByTokenAsync(RegisterRequestDto dto);
        Task<TokenResponseDto> LoginByTokenAsync(LoginRequestDto dto);
    }
}
