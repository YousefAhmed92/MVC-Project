using company.services.Interfaces.Employee.Dto;
using Company.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace company.services.Interfaces.Department.Dto
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public ICollection<EmployeeDto>? employees { get; set; } = new LinkedList<EmployeeDto>();

    }
}
