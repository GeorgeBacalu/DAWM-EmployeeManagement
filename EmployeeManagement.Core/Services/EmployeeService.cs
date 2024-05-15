using AutoMapper;
using EmployeeManagement.Core.Mappings;
using EmployeeManagement.Database.Dtos;
using EmployeeManagement.Database.Entities;
using EmployeeManagement.Database.Repositories;

namespace EmployeeManagement.Core.Services
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly RoleRepository _roleRepository;
        private readonly AuthorityRepository _authorityRepository;
        private readonly IMapper _mapper;

        public EmployeeService(EmployeeRepository employeeRepository, RoleRepository roleRepository, AuthorityRepository authorityRepository, IMapper mapper) => 
            (_employeeRepository, _roleRepository, _authorityRepository, _mapper) = (employeeRepository, roleRepository, authorityRepository, mapper);

        public IList<EmployeeDto> GetAll() => _employeeRepository.GetAll().ToDtos(_mapper);

        public EmployeeDto GetById(int id) => _employeeRepository.GetById(id).ToDto(_mapper);

        public EmployeeDto Add(EmployeeDto employeeDto)
        {
            Employee employeeToSave = employeeDto.ToEntity(_roleRepository, _authorityRepository, _mapper);
            _employeeRepository.LinkRoleToEmployee(employeeToSave);
            _employeeRepository.LinkAuthoritiesToEmployee(employeeToSave);
            return _employeeRepository.Add(employeeToSave).ToDto(_mapper);
        }

        public EmployeeDto UpdateById(EmployeeDto employeeDto, int id) => _employeeRepository.UpdateById(employeeDto.ToEntity(_roleRepository, _authorityRepository, _mapper), id).ToDto(_mapper);

        public EmployeeDto DisableById(int id) => _employeeRepository.DisableById(id).ToDto(_mapper);
    }
}