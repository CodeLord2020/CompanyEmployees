
using Contracts;
using Entities.Exceptions;
using Entities.Models;

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

        public Employee GetEmployee(Guid employeeId, Guid companyId, bool trackChanges)
        {
            var employee = FindByCondition(e => e.Id == employeeId && e.CompanyId == companyId,  trackChanges).SingleOrDefault();
            if (employee is null)
            {
                throw new EmployeeNotFoundException(employeeId);
            }
            return employee;
        }

        public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges) =>
        
            FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges).OrderBy(e => e.Name).ToList(); 
        
    }
}