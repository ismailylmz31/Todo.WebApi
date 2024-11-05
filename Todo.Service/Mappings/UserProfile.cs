using AutoMapper;
using Todo.Models.Entities;
using Todo.Models.Users;

namespace Todo.Service.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponseDto>(); // User -> UserResponseDto dönüşümü
            CreateMap<RegisterRequestDto, User>(); // RegisterRequestDto -> User dönüşümü

            // UserResponseDto -> User dönüşümü
            CreateMap<UserResponseDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
