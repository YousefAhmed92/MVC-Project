using company.repo.Interface;
using Company.Data.Contexts;
using Company.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace company.repo.Repo
{
    public class EmployeeRepo : GenericRepo<Employees> , IEmployeeRepo
    {
        //private readonly CompanyDBContext _context;

        public EmployeeRepo(CompanyDBContext context) : base(context) 
        {
            //_context = context;
        }
       

    }
}
