using AutoMapper;
using EmployeeManagement.Core.Mappings;
using EmployeeManagement.Database.Dtos.Common;
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

        public IList<EmployeeDto> GetAll()
        {
            IList<EmployeeDto> employeeDtos = _employeeRepository.GetAll().ToDtos(_mapper);
            return employeeDtos;
        }

        public EmployeeDto GetById(int id)
        {
            EmployeeDto employeeDto = _employeeRepository.GetById(id).ToDto(_mapper);
            return employeeDto;
        }

        public EmployeeDto Add(EmployeeDto employeeDto)
        {
            Employee employeeToAdd = employeeDto.ToEntity(_roleRepository, _authorityRepository, _mapper);
            _employeeRepository.LinkRoleToEmployee(employeeToAdd);
            _employeeRepository.LinkAuthoritiesToEmployee(employeeToAdd);
            EmployeeDto addedEmployeeDto = _employeeRepository.Add(employeeToAdd).ToDto(_mapper);
            return addedEmployeeDto;
        }

        public EmployeeDto UpdateById(EmployeeDto employeeDto, int id)
        {
            Employee employeeToUpdate = employeeDto.ToEntity(_roleRepository, _authorityRepository, _mapper);
            EmployeeDto updatedEmployeeDto = _employeeRepository.UpdateById(employeeToUpdate, id).ToDto(_mapper);
            return updatedEmployeeDto;
        }

        public EmployeeDto DisableById(int id)
        {
            EmployeeDto disabledEmployeeDto = _employeeRepository.DisableById(id).ToDto(_mapper);
            return disabledEmployeeDto;
        }
    }
}