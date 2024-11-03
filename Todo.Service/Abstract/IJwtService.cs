using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models.Entities;
using Todo.Models.Tokens;

namespace Todo.Service.Abstract
{
    public interface IJwtService
    {
        Task<TokenResponseDto> CreateToken(User user);
    }

}
