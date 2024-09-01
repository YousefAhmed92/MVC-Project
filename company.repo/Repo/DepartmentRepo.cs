using company.repo.Interface;
using Company.Data.Contexts;
using Company.Data.Models;

namespace company.repo.Repo
{
    public class DepartmentRepo : GenericRepo<Department> , IDepartmentRepo
    {
        private readonly CompanyDBContext _context;

        public DepartmentRepo(CompanyDBContext context) : base(context) 
        {
            _context = context;
        }


    }
}
