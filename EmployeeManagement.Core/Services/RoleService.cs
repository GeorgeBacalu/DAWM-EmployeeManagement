using AutoMapper;
using EmployeeManagement.Core.Mappings;
using EmployeeManagement.Database.Dtos.Common;
using EmployeeManagement.Database.Entities;
using EmployeeManagement.Database.Repositories;

namespace EmployeeManagement.Core.Services
{
    public class RoleService
    {
        private readonly RoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(RoleRepository roleRepository, IMapper mapper) => (_roleRepository, _mapper) = (roleRepository, mapper);

        public IList<RoleDto> GetAll()
        {
            IList<RoleDto> roleDtos = _roleRepository.GetAll().ToDtos(_mapper);
            return roleDtos;
        }

        public RoleDto GetById(int id)
        {
            RoleDto roleDto = _roleRepository.GetById(id).ToDto(_mapper);
            return roleDto;
        }

        public RoleDto Add(RoleDto roleDto)
        {
            Role roleToAdd = roleDto.ToEntity(_mapper);
           RoleDto addRoleDto = _roleRepository.Add(roleToAdd).ToDto(_mapper);
            return addRoleDto;
        }
    }
}