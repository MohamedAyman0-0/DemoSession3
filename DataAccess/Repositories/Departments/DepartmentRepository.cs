using DataAccess.Repositories.Departments;
using DemoSession3.DataAccess.Data.Contexts;
using DemoSession3.DataAccess.Models.Departments;
using DemoSession3.DataAccess.Repositories.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSession3.DataAccess.Repositories.Departments
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext) //1.Injection
                                                                                      // Ask CLR for Creation ApplicationDbContext object
        {
            _dbContext = dbContext;
        }




    }
}

