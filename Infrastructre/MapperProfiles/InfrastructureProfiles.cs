using AutoMapper;
using Domain.Dto;
using Domain.Dtos;
using Domain.Entities;

namespace Infrastructre.MapperProfiles
{
    public class InfrastructureProfiles:Profile
    {
        public InfrastructureProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<UserLogin, UserLogInDto>().ReverseMap();
            CreateMap<UserRole, UserRoleDto>().ReverseMap();
            CreateMap<RolePermission, RolePermissionDto>().ReverseMap();
            CreateMap<Permission, PermissionDto>().ReverseMap();
        }

    }
}
