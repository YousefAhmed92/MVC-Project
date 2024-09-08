using company.repo.Interface;
using Company.Data.Contexts;
using Company.Data.Models;

namespace company.repo.Repo
{
    public class EmployeeRepo : GenericRepo<Employees> , IEmployeeRepo
    {
        private readonly CompanyDBContext _context;

        public EmployeeRepo(CompanyDBContext context) : base(context) 
        {
            _context = context;
        }

        public IEnumerable<Employees> GetEmployeeByAddress(int address)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employees> GetEmployeeByName(string name)
            => _context.Employees.Where(x => x.Name.Trim().ToLower().Contains(name.Trim().ToLower())).ToList();
    }



}
