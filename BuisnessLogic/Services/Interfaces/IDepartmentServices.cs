using DemoSession3.BuisnessLogic.DataTransferObjects.Departments;

namespace DemoSession3.BuisnessLogic.Services.Interfaces
{
    public interface IDepartmentServices
    {
        int AddDepartment(CreatedDepartmentDto departmentDto);
        bool DeleteDepartment(int Id);
        IEnumerable<DepartmentDto> GetAllDepartments();
        DeparmentDetailsDto? GetDepartmentById(int Id);
        int UpdateDepartment(UpdatedDepartmentDto departmentDto);
    }
}