using DemoSession3.BuisnessLogic.DataTransferObjects.Departments;
using DemoSession3.DataAccess.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DemoSession3.BuisnessLogic.Factories
{
    internal static class DepartmentFactory
    {
        public static DeparmentDetailsDto ToDeparmentDetailsDto(this Department department)
        {

            return new DeparmentDetailsDto()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreatedBy = department.CreatedBy,
                LastModifiedBy = department.LastModifiedBy,
                DateofCreation = DateOnly.FromDateTime(department.CreatedOn ?? DateTime.Now),
                IsDeleted = department.IsDeleted

            };


        }

        public static DepartmentDto ToDeparrtmentDto(this Department department)
        {
            return new DepartmentDto()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateofCreation = DateOnly.FromDateTime(department.CreatedOn ?? DateTime.Now)
            };
        }

        public static Department ToEntity(this CreatedDepartmentDto departmentDto)
        {
            return new Department()
            {
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreatedOn = departmentDto.DateofCreation.ToDateTime(new TimeOnly())

            };
        }

        public static Department ToEntity(this UpdatedDepartmentDto departmentDto)
        {
            return new Department()
            {
                Id = departmentDto.Id,
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreatedOn = departmentDto.DateofCreation.ToDateTime(new TimeOnly())



            };
        }
    }


}