using AutoMapper;
using EmployeeManagement.Core.Mappings;
using EmployeeManagement.Database.Dtos.Common;
using EmployeeManagement.Database.Repositories;

namespace EmployeeManagement.Core.Services
{
    public class RoleService
    {
        private readonly RoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(RoleRepository roleRepository, IMapper mapper) => (_roleRepository, _mapper) = (roleRepository, mapper);

        public IList<RoleDto> GetAll() => _roleRepository.GetAll().ToDtos(_mapper);

        public RoleDto GetById(int id) => _roleRepository.GetById(id).ToDto(_mapper);

        public RoleDto Add(RoleDto roleDto) => _roleRepository.Add(roleDto.ToEntity(_mapper)).ToDto(_mapper);
    }
}