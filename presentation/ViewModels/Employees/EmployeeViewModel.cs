using DemoSession3.DataAccess.Models.Employees;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using System.ComponentModel.DataAnnotations;
using System.Reflection;


namespace presentation.ViewModels.Employee
{
    public class EmployeeViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name should be less than 50 char")]
        [MinLength(3, ErrorMessage = "Name should be at least 3 char")]
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        //123-Street-City-Country
        [RegularExpression(@"^[1-9]{1,3}-[a-zA-Z\s]{2,}-[a-zA-Z\s]{2,}-[a-zA-Z\s]{2,}$",
    ErrorMessage = "Address should be in format '123-Street-City-Country'")]
        public string? Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }
        public DemoSession3.DataAccess.Models.Employees.Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }
    }
}