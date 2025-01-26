
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;

namespace Repository
{
    internal sealed class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext): base (repositoryContext)
        {
            
        }

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }

        public IEnumerable<Employee> GetAllEmployees(bool trackChanges)
        {
          return FindAll(trackChanges).OrderBy(e => e.Name).ToList();
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(EmployeeParameters employeeParameters,
        bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(e => e.Name).
            Skip((employeeParameters.pageNumber - 1) * employeeParameters.PageSize).
            Take(employeeParameters.PageSize).
            ToListAsync();
        }

        public Employee GetEmployee(Guid employeeId, Guid companyId, bool trackChanges)
        {
            var employee = FindByCondition(e => e.Id == employeeId && e.CompanyId == companyId,  trackChanges).SingleOrDefault()
            ?? throw new EmployeeNotFoundException(employeeId);
            return employee;
        }

        public async Task<Employee> GetEmployeeAsync(Guid employeeId, Guid companyId, bool trackChanges)
        {
            return await FindByCondition(e => e.Id == employeeId && e.CompanyId == companyId,  trackChanges).SingleOrDefaultAsync() 
            ?? throw new EmployeeNotFoundException(employeeId);
        }

        public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges) =>
        
            FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges).OrderBy(e => e.Name).ToList();
            // [.. FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges).OrderBy(e => e.Name)];

        public async Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters,
        bool trackChanges)
        {
            var employees =  await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges).
            OrderBy(e => e.Name).
            ToListAsync();   

            return PagedList<Employee>
            .ToPagedList(employees, employeeParameters.pageNumber, employeeParameters.PageSize);

        }

    }
}