using AutoMapper;
using company.services.Interfaces.Department.Dto;
using Company.Data.Models;

namespace company.services.Mapping
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department,DepartmentDto>().ReverseMap();

        }

    }
}
