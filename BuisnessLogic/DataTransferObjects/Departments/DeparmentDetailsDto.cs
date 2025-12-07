using DemoSession3.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSession3.BuisnessLogic.DataTransferObjects.Departments
{
    public class DeparmentDetailsDto
    {


        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public DateOnly DateofCreation { get; set; }
        public int LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }

    }
}