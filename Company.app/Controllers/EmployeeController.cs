using company.services.Interfaces;
using company.services.Interfaces.Department.Dto;
using company.services.Interfaces.Employee.Dto;
using Company.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;

namespace Company.app.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService employeeService , IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        public IActionResult Index(string SearchInput)
        {


            ViewBag.Departments = _departmentService.GetAll().ToList();


            IEnumerable<EmployeeDto> employees = new List<EmployeeDto>();
            if (string.IsNullOrEmpty(SearchInput))
                employees = _employeeService.GetAll();

            else
                employees = _employeeService.GetEmployeeByName(SearchInput);
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = _departmentService.GetAll();
            return View();

        }

        [HttpPost]
        public IActionResult Create(EmployeeDto employees)
        {

            try
            {
                if(ModelState.IsValid)
                {
                    _employeeService.Add(employees);
                    return RedirectToAction(nameof(Index));
                }
                return View(employees);

            }
            catch (Exception ex)
            {
                return View(employees);
            }
        }

        public IActionResult Details(int id , string viewName = "Details" )
        {
            ViewBag.Departments = _departmentService.GetAll().ToList();

            var employees = _employeeService.GetById(id);

            if (employees is null)
                return RedirectToAction("NotFoundPage", null, "Home");
            return View(viewName, employees);

        }

        [HttpGet]
        public IActionResult Update(int Id )
        {
            return Details(Id, "Update");
        }

        //[HttpPost]
        //public IActionResult Update(int? id , EmployeeDto employeeDto)
        //{
        //    if(employeeDto.Id != id.Value)
        //        return RedirectToAction("NotFoundPage", null, "Home");
        //    _employeeService.Update(employeeDto);
        //    return RedirectToAction(nameof(Index));
        //}
        [HttpPost]
        public IActionResult Update(int? id, EmployeeDto employeeDto)
        {

            ViewBag.Departments = _departmentService.GetAll().ToList();


            if (employeeDto.Id != id.Value)
                return RedirectToAction("NotFoundPage", null, "Home");


            if (employeeDto.Id == null)
                return NotFound();

            if (employeeDto.Image != null && employeeDto.Image.Length > 0)
            {
                var fileName = Path.GetFileName(employeeDto.Image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/Images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    employeeDto.Image.CopyTo(stream);
                }

                employeeDto.ImageUrl = fileName;
            }
            else
            {
                // Retain the existing image URL
                employeeDto.ImageUrl = employeeDto.ImageUrl;
            }

            _employeeService.Update(employeeDto);
            return RedirectToAction(nameof(Index));
        }






        public IActionResult Delete(int id)
        {
            var employees= _employeeService.GetById(id);
            _employeeService.Delete(employees);
            return RedirectToAction(nameof(Index));
        }


    }
}
