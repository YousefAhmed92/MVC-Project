using AutoMapper;
using company.services.Interfaces.Employee.Dto;
using Company.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace company.services.Mapping.Employee
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employees, EmployeeDto>().ReverseMap();
        }
    }
}
