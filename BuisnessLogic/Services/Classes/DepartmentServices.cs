using DemoSession3.BuisnessLogic.DataTransferObjects.Departments;
using DemoSession3.BuisnessLogic.Factories;
using DemoSession3.BuisnessLogic.Services.Interfaces;
using DemoSession3.DataAccess.Repositories.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DemoSession3.BuisnessLogic.Services.Classes
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentServices(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAll();
            var departmentstoreturn = departments.Select(D => D.ToDeparrtmentDto());
            return departmentstoreturn;
        }

        public DeparmentDetailsDto? GetDepartmentById(int Id)
        {
            var department = _departmentRepository.GetById(Id);

            //if (department == null)
            //{
            //    return null;
            //}
            //else
            //{
            //    return new DeparmentDetailsDto()
            //    {
            //        Id = department.Id,
            //        Name = department.Name,
            //        Code = department.Code,
            //        Description = department.Description,
            //        CreatedBy = department.CreatedBy,
            //        LastModifiedBy = department.LastModifiedBy,
            //        DateofCreation = DateOnly.FromDateTime(department.CreatedOn ?? DateTime.Now),
            //        IsDeleted = department.IsDeleted

            //    };

            // Manual Mapping
            //Auto Mapper
            //Constructor Mapping
            // Extension Method 
            return department == null ? null : department.ToDeparmentDetailsDto();


        }

        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            return _departmentRepository.Add(departmentDto.ToEntity());
        }
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            return _departmentRepository.Update(departmentDto.ToEntity());
        }
        public bool DeleteDepartment(int Id)
        {
            var department = _departmentRepository.GetById(Id);
            if (department == null)
            {
                return false;
            }
            else
            {
                var Result = _departmentRepository.Remove(department);
                return Result > 0 ? true : false;
            }


        }
    }
}