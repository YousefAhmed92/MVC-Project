using company.repo.Interface;
using Company.Data.Contexts;

namespace company.repo.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyDBContext _Context;

        public UnitOfWork(CompanyDBContext context)
        {
            _Context = context;
            employeeRepo = new EmployeeRepo(context);
            departmentRepo = new DepartmentRepo(context);

        }

        public IDepartmentRepo departmentRepo { get ; set ; }
        public IEmployeeRepo employeeRepo { get; set ; }

        public int Complete()
            => _Context.SaveChanges();
    }
}
