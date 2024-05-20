using AutoMapper;
using EmployeeManagement.Core.Mappings;
using EmployeeManagement.Database.Dtos.Common;
using EmployeeManagement.Database.Entities;
using EmployeeManagement.Database.Repositories;

namespace EmployeeManagement.Core.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly RoleRepository _roleRepository;
        private readonly AuthorityRepository _authorityRepository;
        private readonly IMapper _mapper;

        public UserService(UserRepository userRepository, IMapper mapper) => (_userRepository, _mapper) = (userRepository, mapper);

        public IList<UserDto> GetAll() => _userRepository.GetAll().ToDtos(_mapper);

        public UserDto GetById(int id) => _userRepository.GetById(id).ToDto(_mapper);

        public UserDto Add(UserDto userDto)
        {
            User userToAdd = userDto.ToEntity(_roleRepository, _authorityRepository, _mapper);
            _userRepository.LinkRoleToUser(userToAdd);
            _userRepository.LinkAuthoritiesToUser(userToAdd);
            return _userRepository.Add(userToAdd).ToDto(_mapper);
        }

        public UserDto UpdateById(UserDto userDto, int id) => _userRepository.UpdateById(userDto.ToEntity(_roleRepository, _authorityRepository, _mapper), id).ToDto(_mapper);

        public UserDto DisableById(int id) => _userRepository.DisableById(id).ToDto(_mapper);
    }
}