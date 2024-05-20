using AutoMapper;
using EmployeeManagement.Database.Dtos.Common;
using EmployeeManagement.Database.Entities;
using EmployeeManagement.Database.Repositories;

namespace EmployeeManagement.Core.Mappings
{
    public static class EmployeeMappingExtensions
    {
        public static IList<EmployeeDto> ToDtos(this IList<Employee> employees, IMapper mapper) => employees.Select(employee => employee.ToDto(mapper)).ToList();

        public static IList<Employee> ToEntities(this IList<EmployeeDto> employeeDtos, RoleRepository roleRepository, AuthorityRepository authorityRepository, IMapper mapper) => 
            employeeDtos.Select(employeeDto => employeeDto.ToEntity(roleRepository, authorityRepository, mapper)).ToList();

        public static EmployeeDto ToDto(this Employee employee, IMapper mapper)
        {
            EmployeeDto employeeDto = mapper.Map<EmployeeDto>(employee);
            employeeDto.AuthoritiesIds = employee.Authorities.Select(authority => authority.Id).ToList();
            return employeeDto;
        }

        public static Employee ToEntity(this EmployeeDto employeeDto, RoleRepository roleRepository, AuthorityRepository authorityRepository, IMapper mapper)
        {
            Employee employee = mapper.Map<Employee>(employeeDto);
            employee.Role = roleRepository.GetById(employeeDto.RoleId);
            employee.Authorities = employeeDto.AuthoritiesIds.Select(authorityRepository.GetById).ToList();
            return employee;
        }
    }
}