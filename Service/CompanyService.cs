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
    internal sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public CompanyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;

        }

        public CompanyDTO CreateCompany(CompanyForCreationDto company)
        {
            var maptocompany = _mapper.Map<Company>(company);
            _repository.Company.CreateCompany(maptocompany);
            _repository.Save();

            var companyToReturn = _mapper.Map<CompanyDTO>(maptocompany);
            return companyToReturn;

        }

        public IEnumerable<CompanyDTO> GetAllCompanies(bool trackChanges)
        {
           try {

                var companies = _repository.Company.GetAllCompanies(trackChanges);
                // var companiesDTO = companies.Select(company => new CompanyDTO(
                //     company.Id, company.Name ?? "", string.Join(' ', company.Address, company.Country))).ToList();
                var companiesDTO = _mapper.Map<IEnumerable<CompanyDTO>>(companies);
                Console.WriteLine("Hello", companies);
                return companiesDTO;
           }
           catch (Exception ex) {

                _logger.LogError($"Something went wrong in the {nameof(GetAllCompanies)} service method {ex}");
                throw;
           }

        }

        public IEnumerable<CompanyDTO> GetCompaniesById(IEnumerable<Guid> Ids, bool trackChanges)
        {
            if(Ids is null){
               throw new IdParametersBadRequestException();
            }
            var companies = _repository.Company.GetByIds(Ids, trackChanges);
            if (Ids.Count() != companies.Count()){
                throw new CollectionByIdsBadRequestException();
            }

            var companiesToReturn = _mapper.Map<IEnumerable<CompanyDTO>>(companies);
            return companiesToReturn;

        }

        public CompanyDTO GetCompany(Guid Id, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(Id, trackChanges);
            if (company is null) {
                throw new CompanyNotFoundException(Id);
            }

            var companyDTO = _mapper.Map<CompanyDTO>(company);
            return companyDTO;
        }
    }
}