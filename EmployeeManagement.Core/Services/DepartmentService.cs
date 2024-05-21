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

        public IList<DepartmentDto> GetAll()
        {
            IList<DepartmentDto> departmentDtos = _departmentRepository.GetAll().ToDtos(_mapper);
            return departmentDtos;
        }

        public DepartmentDto GetById(int id)
        {
            DepartmentDto departmentDto = _departmentRepository.GetById(id).ToDto(_mapper);
            return departmentDto;
        }

        public DepartmentDto Add(DepartmentDto departmentDto)
        {
            Department departmentToAdd = departmentDto.ToEntity(_employeeRepository, _mapper);
            _departmentRepository.LinkEmployeesToDepartment(departmentToAdd);
            DepartmentDto addedDepartmentDto = _departmentRepository.Add(departmentToAdd).ToDto(_mapper);
            return addedDepartmentDto;
        }

        public DepartmentDto UpdateById(DepartmentDto departmentDto, int id)
        {
            Department departmentToUpdate = departmentDto.ToEntity(_employeeRepository, _mapper);
            DepartmentDto updatedDepartmentDto = _departmentRepository.UpdateById(departmentToUpdate, id).ToDto(_mapper);
            return updatedDepartmentDto;
        }

        public DepartmentDto DisableById(int id)
        {
            DepartmentDto disabledDepartmentDto = _departmentRepository.DisableById(id).ToDto(_mapper);
            return disabledDepartmentDto;
        }
    }
}