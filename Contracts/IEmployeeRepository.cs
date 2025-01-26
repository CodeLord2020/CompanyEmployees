using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges);
        IEnumerable<Employee> GetAllEmployees(bool trackChanges);
        Employee GetEmployee(Guid employeeId, Guid companyId, bool trackChanges);
        void CreateEmployeeForCompany(Guid companyId, Employee employee);
        void DeleteEmployee(Employee employee);

        Task<PagedList<IEnumerable<Employee>>> GetEmployeesAsync(Guid companyId,
        EmployeeParameters employeeParameters, bool trackChanges);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync(EmployeeParameters employeeParameters, bool trackChanges);
        Task<Employee> GetEmployeeAsync(Guid employeeId, Guid companyId, bool trackChanges);

    }
}