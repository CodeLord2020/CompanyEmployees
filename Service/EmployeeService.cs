using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public EmployeeService(IRepositoryManager repository,
         ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }


        public EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeeForCreationDto employee, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }
            var maptoemployee = _mapper.Map<Employee>(employee);
            _repository.Employee.CreateEmployeeForCompany(companyId, maptoemployee);
            _repository.Save();

            var employeeToReturn = _mapper.Map<EmployeeDto>(maptoemployee);
            return employeeToReturn;
        }

        public EmployeeDto GetEmployee(Guid employeeId, Guid companyId, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }
            var employee = _repository.Employee.GetEmployee(employeeId, companyId, trackChanges);
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }

        public IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges)
        {
            var companies = _repository.Company.GetCompany(companyId, trackChanges);
            if (companies is null){
                  throw new CompanyNotFoundException(companyId);
            }
            var employees = _repository.Employee.GetEmployees(companyId, trackChanges);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return employeesDto;
        }

        EmployeeDto IEmployeeService.GetEmployee(Guid employeeId, Guid companyId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        IEnumerable<EmployeeDto> IEmployeeService.GetEmployees(Guid companyId, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}