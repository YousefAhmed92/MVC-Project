using AutoMapper;
using company.repo.Interface;
using company.services.Helper;
using company.services.Interfaces;
using company.services.Interfaces.Employee.Dto;
using Company.Data.Models;

namespace company.services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(EmployeeDto employeeDto)
        {
            //Employees employees = new Employees
            //{
            //    Name = employeeDto.Name,
            //    Address = employeeDto.Address,
            //    PhoneNumber = employeeDto.PhoneNumber,
            //    Salary = employeeDto.Salary,
            //    Email = employeeDto.Email,
            //    ImageUrl = employeeDto.ImageUrl,
            //    HiringDate = employeeDto.HiringDate
            //};
            employeeDto.ImageUrl = DocumentSetting.UploadFile(employeeDto.Image, "Images");

            Employees employees = _mapper.Map<Employees>(employeeDto);

            _unitOfWork.employeeRepo.Add(employees);
            _unitOfWork.Complete();
        }

        public void Delete(EmployeeDto employeeDto)
        {

            var employees = _unitOfWork.employeeRepo.GetById(employeeDto.Id);
            _unitOfWork.employeeRepo.Delete(employees);
            _unitOfWork.Complete();
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            var employees = _unitOfWork.employeeRepo.GetAll();
            IEnumerable<EmployeeDto> mappedEmployees = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            //var mappedEmployees = employees.Select(x => new EmployeeDto
            //{
            //    Name = x.Name,
            //    Address = x.Address,
            //    PhoneNumber = x.PhoneNumber,
            //    Salary = x.Salary,
            //    Email = x.Email,
            //    ImageUrl = x.ImageUrl,
            //    HiringDate = x.HiringDate,
            //    DepartmentId = x.DepartmentId,
            //    Id = x.Id,
            //    CreatedAt = x.CreatedAt,
            //    Age = x.Age,

                

            //});


            return mappedEmployees;
        }

        public EmployeeDto GetById(int? id)
        {
            if (id is null)
                return null;

            var employees = _unitOfWork.employeeRepo.GetById(id.Value);

            if (employees is null)
                return null;

            EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employees);
            return employeeDto;

            //EmployeeDto employeeDto  = new EmployeeDto
            //{
            //    Name = employees.Name,
            //    Address = employees.Address,
            //    PhoneNumber = employees.PhoneNumber,
            //    Salary = employees.Salary,
            //    Email = employees.Email,
            //    ImageUrl = employees.ImageUrl,
            //    HiringDate = employees.HiringDate
            //};
            //return employeeDto;
        }

        public IEnumerable<EmployeeDto> GetEmployeeByName(string name)
        {
            var employees = _unitOfWork.employeeRepo.GetEmployeeByName(name);
            IEnumerable<EmployeeDto> mappedEmployees = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return mappedEmployees;



            //var mappedEmployees = employees.Select(x => new EmployeeDto
            //{
            //    Name = x.Name,
            //    Address = x.Address,
            //    PhoneNumber = x.PhoneNumber,
            //    Salary = x.Salary,
            //    Email = x.Email,
            //    ImageUrl = x.ImageUrl,
            //    HiringDate = x.HiringDate,
            //    DepartmentId = x.DepartmentId,
            //    Id = x.Id,
            //    CreatedAt = x.CreatedAt,
            //    Age = x.Age,
            //});

            //return mappedEmployees;
        }
        public void Update(EmployeeDto employeeDto)
        {
            //var name = employeeDto.Name;
            //var PhoneNumber = employeeDto.PhoneNumber;
            //var salary = employeeDto.Salary;
            //var address = employeeDto.Address;
            //var img = employeeDto.Image;
            //var imgURL = employeeDto.ImageUrl;
            //var deptId= employeeDto.DepartmentId;
            //var date = employeeDto.HiringDate;
            //var createdAt = DateTime.Now;

            var mapping = _mapper.Map<Employees>(employeeDto);
            if (mapping is null)
                throw new Exception("department is not found");
            _unitOfWork.employeeRepo.Update(mapping);
            _unitOfWork.Complete();
        }



    }
}
