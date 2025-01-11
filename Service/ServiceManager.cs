using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Service.Contracts;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICompanyService> _companyService;
        private readonly Lazy<IEmployeeService> _employeeService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _companyService = new Lazy<ICompanyService>(() => new CompanyService(repositoryManager, logger));
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(repositoryManager, logger));
        }
        public ICompanyService CompanyService => throw new NotImplementedException();

        public IEmployeeService EmployeeService => throw new NotImplementedException();
    }
}