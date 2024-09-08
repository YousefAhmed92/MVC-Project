using company.services.Interfaces;
using Company.Data.Models;
using company.repo.Interface;
using company.services.Interfaces.Department.Dto;
using AutoMapper;

namespace company.services.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public void Add(DepartmentDto departmentDto)
        {
            // from departmentDto  => to Department

            var MappedDepartment = _mapper.Map<Department>(departmentDto);
            _unitOfWork.departmentRepo.Add(MappedDepartment);
            _unitOfWork.Complete();
        }

        public void Delete(DepartmentDto departmentDto)
        {
            // from departmentDto  => to Department

            var existingDepartment = _unitOfWork.departmentRepo.GetById(departmentDto.Id);

            _unitOfWork.departmentRepo.Delete(existingDepartment);
            _unitOfWork.Complete();
        }

        public IEnumerable<DepartmentDto> GetAll()
        {
            // from Department  => to departmentDto

            var departments = _unitOfWork.departmentRepo.GetAll();
            var mappedDepartment = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
            return mappedDepartment;
        }

        public DepartmentDto GetById(int? id)
        {
            // from Department  => to departmentDto

            if (id is null)
                return null;

            var department = _unitOfWork.departmentRepo.GetById(id.Value);

            if (department is null)
                return null;

            var mappedDepartment = _mapper.Map<DepartmentDto>(department);


            return mappedDepartment;

        }

        public void Update(DepartmentDto departmentDto)
        {
            // from departmentDto  => to Department

            var mapping = _mapper.Map<Department>(departmentDto);
            if (mapping == null)
                throw new Exception("Department not found");

            _unitOfWork.departmentRepo.Update(mapping);
            _unitOfWork.Complete();
           
        }
    }
}
