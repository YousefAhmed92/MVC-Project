using company.services.Interfaces.Employee.Dto;
using Company.Data.Models;

namespace company.services.Interfaces
{
    public interface IEmployeeService
    {
        EmployeeDto GetById(int? id);
        IEnumerable<EmployeeDto> GetAll();
        void Add(EmployeeDto employees);
        void Update(EmployeeDto employees);
        void Delete(EmployeeDto employees);
        IEnumerable<EmployeeDto> GetEmployeeByName(string name);

    }
}
