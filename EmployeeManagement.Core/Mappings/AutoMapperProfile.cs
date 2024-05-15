﻿using AutoMapper;
using EmployeeManagement.Database.Dtos;
using EmployeeManagement.Database.Entities;

namespace EmployeeManagement.Core.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Authority, AuthorityDto>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Role, RoleDto>();

            CreateMap<AuthorityDto, Authority>();
            CreateMap<DepartmentDto, Department>();
            CreateMap<EmployeeDto, Employee>();
            CreateMap<RoleDto, Role>();
        }
    }
}