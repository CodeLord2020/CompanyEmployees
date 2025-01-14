using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges);
        Employee GetEmployee(Guid employeeId, Guid companyId, bool trackChanges);

        void CreateEmployeeForCompany(Guid companyId, Employee employee);
    }
}