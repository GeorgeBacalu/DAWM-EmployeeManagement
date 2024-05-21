using AutoMapper;
using EmployeeManagement.Database.Dtos.Common;
using EmployeeManagement.Database.Entities;
using EmployeeManagement.Database.Repositories;

namespace EmployeeManagement.Core.Mappings
{
    public static class UserMappingExtensions
    {
        public static IList<UserDto> ToDtos(this IList<User> users, IMapper mapper)
        {
            IList<UserDto> userDtos = users.Select(user => user.ToDto(mapper)).ToList();
            return userDtos;
        }

        public static IList<User> ToEntities(this IList<UserDto> userDtos, RoleRepository roleRepository, AuthorityRepository authorityRepository, IMapper mapper)
        {
            IList<User> users = userDtos.Select(userDto => userDto.ToEntity(roleRepository, authorityRepository, mapper)).ToList();
            return users;
        }

        public static UserDto ToDto(this User user, IMapper mapper)
        {
            UserDto userDto = mapper.Map<UserDto>(user);
            userDto.AuthoritiesIds = user.Authorities.Select(authority => authority.Id).ToList();
            return userDto;
        }

        public static User ToEntity(this UserDto userDto, RoleRepository roleRepository, AuthorityRepository authorityRepository, IMapper mapper)
        {
            User user = mapper.Map<User>(userDto);
            user.Role = roleRepository.GetById(userDto.RoleId);
            user.Authorities = userDto.AuthoritiesIds.Select(authorityRepository.GetById).ToList();
            return user;
        }
    }
}