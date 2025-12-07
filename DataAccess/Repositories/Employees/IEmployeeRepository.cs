using DemoSession3.DataAccess.Models.Employees;
using DemoSession3.DataAccess.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSession3.DataAccess.Repositories.Employees
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {

    }
}