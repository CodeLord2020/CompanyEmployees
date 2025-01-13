
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

        public Employee GetEmployee(Guid employeeId, Guid companyId, bool trackChanges)
        {
            var employee = FindByCondition(e => e.Id == employeeId && e.CompanyId == companyId,  trackChanges).SingleOrDefault();
            if (employee == null)
            {
                throw new EmployeeNotFoundException(employeeId);
            }
            return employee;
        }

        public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges) =>
        
            FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges).OrderBy(e => e.Name).ToList(); 
        
    }
}