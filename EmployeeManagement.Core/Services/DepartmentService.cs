using AutoMapper;
using EmployeeManagement.Core.Mappings;
using EmployeeManagement.Database.Dtos.Common;
using EmployeeManagement.Database.Entities;
using EmployeeManagement.Database.Repositories;

namespace EmployeeManagement.Core.Services
{
    public class DepartmentService
    {
        private readonly DepartmentRepository _departmentRepository;
        private readonly EmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public DepartmentService(DepartmentRepository departmentRepository, EmployeeRepository employeeRepository, IMapper mapper) => 
            (_departmentRepository, _employeeRepository, _mapper) = (departmentRepository, employeeRepository, mapper);

        public IList<DepartmentDto> GetAll() => _departmentRepository.GetAll().ToDtos(_mapper);

        public DepartmentDto GetById(int id) => _departmentRepository.GetById(id).ToDto(_mapper);

        public DepartmentDto Add(DepartmentDto departmentDto)
        {
            Department departmentToAdd = departmentDto.ToEntity(_employeeRepository, _mapper);
            _departmentRepository.LinkEmployeesToDepartment(departmentToAdd);
            return _departmentRepository.Add(departmentToAdd).ToDto(_mapper);
        }

        public DepartmentDto UpdateById(DepartmentDto departmentDto, int id) => _departmentRepository.UpdateById(departmentDto.ToEntity(_employeeRepository, _mapper), id).ToDto(_mapper);

        public DepartmentDto DisableById(int id) => _departmentRepository.DisableById(id).ToDto(_mapper);
    }
}