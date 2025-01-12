using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    internal sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public CompanyService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IEnumerable<CompanyDTO> GetAllCompanies(bool trackChanges)
        {
           try {

                var companies = _repository.Company.GetAllCompanies(trackChanges);
                var companiesDTO = companies.Select(company => new CompanyDTO(
                    company.Id, company.Name ?? "", string.Join(' ', company.Address, company.Country))).ToList();
                Console.WriteLine("Hello", companies);
                return companiesDTO;
           }
           catch (Exception ex) {

                _logger.LogError($"Something went wrong in the {nameof(GetAllCompanies)} service method {ex}");
                throw;
           }

        }
    }
}