using DemoSession3.BuisnessLogic.DataTransferObjects.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSession3.BuisnessLogic.Services.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName, bool withTracking = false);
        EmployeeDetailsDto? GetEmployeeById(int Id);
        int CreateEmployee(CreatedEmployeeDto employeeDto);
        int UpdateEmployee(UpdatedEmployeeDto employeeDto);
        bool DeleteEmployee(int Id);


    }
}