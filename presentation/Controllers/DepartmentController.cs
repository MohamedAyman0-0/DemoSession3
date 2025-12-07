
using DemoSession3.BuisnessLogic.DataTransferObjects;
using DemoSession3.BuisnessLogic.DataTransferObjects.Departments;
using DemoSession3.BuisnessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using presentation.ViewModels.Department;
using System.Linq.Expressions;

namespace DemoSession3.Presentation.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentServices _departmentServices;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IDepartmentServices departmentServices,
            ILogger<DepartmentController> logger,
           IWebHostEnvironment env)
        {
            _departmentServices = departmentServices;
            _logger = logger;
            _env = env;
        }
        //Action=> master action
        //Get:baseUrl/Departments/Index
        public IActionResult Index()
        {
            ViewData["Message"] = new DepartmentDto() { Name = "Hello from viewData" };
            ViewBag.Message = new DepartmentDto() { Name = "Hello from ViewBag" };
            var departments = _departmentServices.GetAllDepartments();
            return View(departments);
        }

        #region Create

        //show the form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
            //server side validation
            if (!ModelState.IsValid)
            {
                return View(departmentVM);
            }
            var message = string.Empty;
            try
            {
                var departmentDto = new CreatedDepartmentDto()
                {
                    Code = departmentVM.Code,
                    Name = departmentVM.Name,
                    Description = departmentVM.Description,
                    DateofCreation = departmentVM.DateofCreation
                };
                var result = _departmentServices.AddDepartment(departmentDto);
                if (result > 0)
                    message = "Department Created Successfully";

                else

                    message = "Department can't be created now,try again later:(";

                TempData["Message"] = message;
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                //log the exception
                _logger.LogError(ex, ex.Message);
                if (_env.IsDevelopment())
                {
                    message = ex.Message;
                    return View(departmentVM);
                }
                else
                {
                    message = "Department cannot be Created";
                    return View("Eroor", message);
                }
            }
        }
        #endregion

        #region Details
        [HttpGet]
        //baseUrl/Department/Details/{Id}
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest(); //400
            }
            var department = _departmentServices.GetDepartmentById(id.Value);
            if (department == null)
            {
                return NotFound(); //404
            }
            return View(department);
        }
        #endregion

        #region Edit
        //baseUrl/Department/Edit/{Id}
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest(); //400
            }
            var department = _departmentServices.GetDepartmentById(id.Value);
            if (department == null)
            {
                return NotFound(); //404
            }

            return View(new DepartmentViewModel()
            {
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateofCreation = department.DateofCreation


            });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken] //Action Filter
        public IActionResult Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentVM);
            }
            var message = string.Empty;
            try
            {
                var result = _departmentServices.UpdateDepartment(new UpdatedDepartmentDto()
                {
                    Id = id,
                    Name = departmentVM.Name,
                    Code = departmentVM.Code,
                    Description = departmentVM.Description ?? string.Empty,
                    DateofCreation = departmentVM.DateofCreation
                });
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Department Cannot be Updated";

                }
            }
            catch (Exception ex)
            {
                message = _env.IsDevelopment() ? ex.Message : "Department cannot be Updated";

            }
            return View(departmentVM);

        }
        #endregion

        #region Delete
        //baseUrl/Department/Delete/{Id}
        [HttpGet]
        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return BadRequest(); //400
            }
            var department = _departmentServices.GetDepartmentById(id.Value);
            if (department == null)
            {
                return NotFound(); //404
            }
            return View(department);

        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var message = string.Empty;
            try
            {
                var result = _departmentServices.DeleteDepartment(id);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "an Error happend when Deleting the Department ";
                    return View(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _env.IsDevelopment() ? ex.Message : "an Error happend when Deleting the Department ";
            }

            return View(nameof(Index));
        }
        #endregion
    }
}