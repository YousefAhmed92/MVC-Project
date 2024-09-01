using company.repo.Interface;
using company.repo.Repo;
using Microsoft.AspNetCore.Mvc;

namespace Company.app.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepo _departmentRepo;

        public DepartmentController(IDepartmentRepo departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }
        public IActionResult Index()
        {
            var department = _departmentRepo.GetAll();
            return View(department);
        }
    }
}
