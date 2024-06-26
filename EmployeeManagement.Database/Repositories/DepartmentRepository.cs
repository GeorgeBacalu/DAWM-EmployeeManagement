﻿using EmployeeManagement.Database.Context;
using EmployeeManagement.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Database.Repositories
{
    public class DepartmentRepository
    {
        private readonly EmployeeManagementDbContext _context;

        public DepartmentRepository(EmployeeManagementDbContext context) => _context = context;

        public IList<Department> GetAll()
        {
            IList<Department> departments = _context.Departments
                .Include(department => department.Employees)
                .Where(department => department.DeletedAt == null)
                .OrderBy(department => department.CreatedAt)
                .ToList();
            return departments;
        }

        public Department GetById(int id)
        {
            Department department = _context.Departments
                .Include(department => department.Employees)
                .Where(department => department.DeletedAt == null)
                .FirstOrDefault(department => department.Id == id)
                ?? throw new Exception($"Department with id {id} not found");
            return department;
        }

        public Department Add(Department department)
        {
            department.CreatedAt = DateTime.Now;
            _context.Departments.Add(department);
            _context.SaveChanges();
            return department;
        }

        public Department UpdateById(Department department, int id)
        {
            Department departmentToUpdate = GetById(id);
            departmentToUpdate.Name = department.Name;
            departmentToUpdate.Description = department.Description;
            if (_context.Entry(departmentToUpdate).State == EntityState.Modified)
                departmentToUpdate.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return departmentToUpdate;
        }

        public Department DisableById(int id)
        {
            Department departmentToDisable = GetById(id);
            departmentToDisable.DeletedAt = DateTime.Now;
            _context.SaveChanges();
            return departmentToDisable;
        }

        public void LinkEmployeesToDepartment(Department department) // for new department, employee entity state should be unchanged
        {
            department.Employees = department.Employees
                .Select(employee => _context.Employees.Find(employee.Id))
                .Where(employee => employee != null)
                .ToList();
            department.Employees.Select(employee => _context.Entry(employee).State = EntityState.Unchanged);
        }
    }
}