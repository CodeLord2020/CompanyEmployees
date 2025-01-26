using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

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

        public async Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDto employee, bool trackChanges)
        {
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges) 
            ?? throw new CompanyNotFoundException(companyId);

            var maptoemployee = _mapper.Map<Employee>(employee);
            _repository.Employee.CreateEmployeeForCompany(companyId, maptoemployee);
            await _repository.SaveAsync();

            var employeeToReturn = _mapper.Map<EmployeeDto>(maptoemployee);
            return employeeToReturn;
        }

        public async Task DeleteEmployeeAsync(Guid employeeId, Guid companyId, bool trackChanges)
        {
           var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges) 
           ?? throw new CompanyNotFoundException(companyId);

            var employee = await _repository.Employee.GetEmployeeAsync(employeeId, companyId, trackChanges)
            ?? throw new EmployeeNotFoundException(employeeId);

            _repository.Employee.DeleteEmployee(employee);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(EmployeeParameters employeeParameters,bool trackChanges)
        {
            var allEmployees = await _repository.Employee.GetAllEmployeesAsync(employeeParameters, trackChanges);
            var allemployeedto = _mapper.Map<IEnumerable<EmployeeDto>>(allEmployees); 
            return allemployeedto;
        }

        public async Task<EmployeeDto> GetEmployeeAsync(Guid employeeId, Guid companyId, bool trackChanges)
        {
            var company = await  _repository.Company.GetCompanyAsync(companyId, trackChanges) 
            ?? throw new CompanyNotFoundException(companyId);

            var employee = await _repository.Employee.GetEmployeeAsync(employeeId, companyId, trackChanges);
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }

        public async Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)>
         GetEmployeeForPatchAsync(Guid companyId, Guid id, bool compTrackChanges, bool empTrackChanges)
        {
            var companyInstance = await  _repository.Company.GetCompanyAsync(companyId, compTrackChanges) 
            ?? throw new CompanyNotFoundException(companyId);

            var employeeInstance = await _repository.Employee.GetEmployeeAsync(id, companyId, empTrackChanges) 
            ?? throw new EmployeeNotFoundException(id);

            var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeInstance);
            return (employeeToPatch, employeeInstance);

        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges)
        {
            var companies = await  _repository.Company.GetCompanyAsync(companyId, trackChanges) 
            ?? throw new CompanyNotFoundException(companyId);
            var employees = await _repository.Employee.GetEmployeesAsync(companyId, employeeParameters, trackChanges);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return employeesDto;
        }

        public async Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)
        {
            _mapper.Map(employeeToPatch, employeeEntity); 
            await _repository.SaveAsync();
        }

        public async Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid employeeId, EmployeeForUpdateDto employeeForUpdate,
         bool compTrackChanges, bool empTrackChanges)
        {
          
            var companyInsatnce = await _repository.Company.GetCompanyAsync(companyId, compTrackChanges) 
            ?? throw new CompanyNotFoundException(companyId);

            var employeeInstance = await _repository.Employee.GetEmployeeAsync(employeeId, companyId, empTrackChanges) 
            ?? throw new EmployeeNotFoundException(employeeId);

            _mapper.Map(employeeForUpdate, employeeInstance);
            await _repository.SaveAsync();

        }
        // EmployeeDto IEmployeeService.GetEmployee(Guid employeeId, Guid companyId, bool trackChanges)
        // {
        //     throw new NotImplementedException();
        // }

        // IEnumerable<EmployeeDto> IEmployeeService.GetEmployees(Guid companyId, bool trackChanges)
        // {
        //     throw new NotImplementedException();
        // }
    }
}