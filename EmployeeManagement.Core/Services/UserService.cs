using AutoMapper;
using EmployeeManagement.Core.Mappings;
using EmployeeManagement.Database.Dtos.Common;
using EmployeeManagement.Database.Dtos.Request;
using EmployeeManagement.Database.Entities;
using EmployeeManagement.Database.Repositories;

namespace EmployeeManagement.Core.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly RoleRepository _roleRepository;
        private readonly AuthorityRepository _authorityRepository;
        private readonly SecurityService _securityService;
        private readonly IMapper _mapper;

        public UserService(UserRepository userRepository, RoleRepository roleRepository, AuthorityRepository authorityRepository, SecurityService securityService, IMapper mapper) =>
            (_userRepository, _roleRepository, _authorityRepository, _securityService, _mapper) = (userRepository, roleRepository, authorityRepository, securityService, mapper);

        public IList<UserDto> GetAll()
        {
            IList<UserDto> userDtos = _userRepository.GetAll().ToDtos(_mapper);
            return userDtos;
        }

        public UserDto GetById(int id)
        {
            UserDto userDto = _userRepository.GetById(id).ToDto(_mapper);
            return userDto;
        }

        public UserDto? Register(RegisterRequest payload)
        {
            if (payload == null) return null;
            byte[] salt = _securityService.GenerateSalt();
            User user = new User
            {
                Name = payload.Name,
                Email = payload.Email,
                Role = _roleRepository.GetById(payload.RoleId),
                Password = _securityService.HashPassword(payload.Password, salt),
                PasswordSalt = Convert.ToBase64String(salt),
                CreatedAt = DateTime.Now
            };
            user.Authorities = _authorityRepository.GetByRole(user.Role);
            _userRepository.LinkRoleToUser(user);
            _userRepository.LinkAuthoritiesToUser(user);
            UserDto registeredUserDto = _userRepository.Add(user).ToDto(_mapper);
            return registeredUserDto;
        }

        public string? Login(LoginRequest payload)
        {
            if (payload == null) return null;
            User user = _userRepository.GetByEmail(payload.Email);
            string hashedPassword = _securityService.HashPassword(payload.Password, Convert.FromBase64String(user.PasswordSalt));
            string? token = _securityService.GetToken(user, user.Role.Type.ToString());
            return hashedPassword == user.Password ? token : throw new Exception("Invalid password");
        }

        public UserDto UpdateById(UserDto userDto, int id)
        {
            User userToUpdate = userDto.ToEntity(_roleRepository, _authorityRepository, _mapper);
            UserDto updatedUserDto = _userRepository.UpdateById(userToUpdate, id).ToDto(_mapper);
            return updatedUserDto;
        }

        public UserDto DisableById(int id)
        {
            UserDto disabledUserDto = _userRepository.DisableById(id).ToDto(_mapper);
            return disabledUserDto;
        }
    }
}