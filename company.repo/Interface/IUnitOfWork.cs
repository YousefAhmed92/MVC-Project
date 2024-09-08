using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace company.repo.Interface
{
    public interface IUnitOfWork
    {
        public IDepartmentRepo departmentRepo {  get; set; }
        public IEmployeeRepo employeeRepo { get; set; }

        int Complete();
    }
}
