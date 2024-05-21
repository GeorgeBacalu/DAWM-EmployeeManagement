using AutoMapper;
using EmployeeManagement.Database.Dtos.Common;
using EmployeeManagement.Database.Entities;
using EmployeeManagement.Database.Repositories;

namespace EmployeeManagement.Core.Mappings
{
    public static class DepartmentMappingExtensions
    {
        public static IList<DepartmentDto> ToDtos(this IList<Department> departments, IMapper mapper)
        {
            IList<DepartmentDto> departmentDtos = departments.Select(department => department.ToDto(mapper)).ToList();
            return departmentDtos;
        }

        public static IList<Department> ToEntities(this IList<DepartmentDto> departmentDtos, EmployeeRepository employeeRepository, IMapper mapper)
        {
            IList<Department> departments = departmentDtos.Select(departmentDto => departmentDto.ToEntity(employeeRepository, mapper)).ToList();
            return departments;
        }

        public static DepartmentDto ToDto(this Department department, IMapper mapper)
        {
            DepartmentDto departmentDto = mapper.Map<DepartmentDto>(department);
            departmentDto.EmployeesIds = department.Employees.Select(employee => employee.Id).ToList();
            return departmentDto;
        }

        public static Department ToEntity(this DepartmentDto departmentDto, EmployeeRepository employeeRepository, IMapper mapper)
        {
            Department department = mapper.Map<Department>(departmentDto);
            department.Employees = departmentDto.EmployeesIds.Select(employeeRepository.GetById).ToList();
            return department;
        }
    }
}