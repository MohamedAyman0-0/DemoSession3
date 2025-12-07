using DemoSession3.BuisnessLogic.DataTransferObjects.Departments;
using DemoSession3.BuisnessLogic.DataTransferObjects.Employees;
using DemoSession3.BuisnessLogic.Services.Interfaces;
using DemoSession3.DataAccess.Models.Employees;
using Microsoft.AspNetCore.Mvc;

using presentation.ViewModels.Employee;
using System.Reflection;

namespace Session3Demo.Presentation.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeesController> _logger;
        private readonly IWebHostEnvironment _env;

        public EmployeesController(IEmployeeService employeeService,
            ILogger<EmployeesController> logger,
            IWebHostEnvironment env
            )
        {
            _employeeService = employeeService;
            _logger = logger;
            _env = env;
        }

        //Master Action
        public IActionResult Index(string? EmployeeSearchName)
        {
            var employees = _employeeService.GetAllEmployees(EmployeeSearchName);
            return View(employees);
        }

        #region Create
        //Get:baseUrl/Employees/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            //server side validation
            if (ModelState.IsValid)
            {
                try
                {
                    var employeeDto = new CreatedEmployeeDto()
                    {
                        Name = employeeVM.Name,
                        Email = employeeVM.Email,
                        Address = employeeVM.Address,
                        PhoneNumber = employeeVM.PhoneNumber,
                        Age = employeeVM.Age,
                        Salary = employeeVM.Salary,
                        IsActive = employeeVM.IsActive,
                        HiringDate = employeeVM.HiringDate,
                        Gender = employeeVM.Gender,
                        EmployeeType = employeeVM.EmployeeType,
                        DepartmentId = employeeVM.DepartmentId,
                    };
                    var result = _employeeService.CreateEmployee(employeeDto);
                    if (result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "can't create employee Right now");
                    }
                }
                catch (Exception ex)
                {
                    if (_env.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                    }
                }
            }
            return View(employeeVM);

        }
        #endregion

        //Get:baseUrl/Employees/Details/{id}
        public IActionResult Details(int? id)
        {
            if (id == null) return BadRequest(); //400
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee == null) return NotFound(); //404

            return View(employee);

        }

        #region Edit 
        //Get:baseUrl/Employees/Edit/{Id}
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null) return BadRequest(); //400
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee == null) return NotFound(); //404
            return View(new EmployeeViewModel()
            {
                Name = employee.Name,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
                DepartmentId = employee.DepartmentId

            });
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel employeeVM)
        {

            if (id == null) return BadRequest(); //400
            if (ModelState.IsValid)
            {
                try
                {
                    var employeeDto = new UpdatedEmployeeDto()
                    {
                        Id = id.Value,
                        Name = employeeVM.Name,
                        Email = employeeVM.Email,
                        Address = employeeVM.Address,
                        Salary = employeeVM.Salary,
                        Age = employeeVM.Age,
                        PhoneNumber = employeeVM.PhoneNumber,
                        IsActive = employeeVM.IsActive,
                        HiringDate = employeeVM.HiringDate,
                        Gender = employeeVM.Gender,
                        EmployeeType = employeeVM.EmployeeType,
                        DepartmentId = employeeVM.DepartmentId


                    };
                    var result = _employeeService.UpdateEmployee(employeeDto);
                    if (result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "can't update employee Right now");
                    }
                }
                catch (Exception ex)
                {
                    if (_env.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                    }

                }
            }

            return View(employeeVM);
        }

        #endregion
        [HttpPost]
        public IActionResult Delete([FromRoute] int? id)
        {
            if (id == null) return BadRequest(); //400
            try
            {
                var result = _employeeService.DeleteEmployee(id.Value);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogError("Employee cannot be deleted right now");
                }
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    _logger.LogError(ex.Message);
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}








