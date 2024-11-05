using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models.Entities;
using Todo.Models.Tokens;
using Todo.Models.Users;
using Todo.Service.Abstract;

namespace Todo.Service.Concretes
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper; 

        public AuthenticationService(IUserService userService, IJwtService jwtService, IMapper mapper)
        {
            _userService = userService;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<TokenResponseDto> LoginByTokenAsync(LoginRequestDto dto)
        {
            var loginResponse = await _userService.LoginAsync(dto);
            var tokenResponse = await _jwtService.CreateToken(loginResponse);
            return tokenResponse;
        }

        public async Task<TokenResponseDto> RegisterByTokenAsync(RegisterRequestDto dto)
        {
            // Yeni kullanıcı oluşturuluyor
            var registerResponse = await _userService.CreateUserAsync(dto);

            // `UserResponseDto` nesnesini alıyoruz
            var userDto = registerResponse.Data;

            // `UserResponseDto`'yu `User` nesnesine dönüştürüyoruz
            var userEntity = _mapper.Map<User>(userDto);

            // `User` nesnesini kullanarak token oluşturuyoruz
            var tokenResponse = await _jwtService.CreateToken(userEntity);
            return tokenResponse;
        }
    }
}
