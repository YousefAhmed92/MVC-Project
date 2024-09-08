using Company.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace company.repo.Interface
{
    public interface IEmployeeRepo : IGenericRepo<Employees>
    {
        IEnumerable<Employees> GetEmployeeByName(string name);
        IEnumerable<Employees> GetEmployeeByAddress(int address);
   

    }
}
