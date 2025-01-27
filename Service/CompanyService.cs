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
using Shared.RequestFeatures;

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

        public async Task<CompanyDTO> CreateCompanyAsync(CompanyForCreationDto company)
        {
            var maptocompany = _mapper.Map<Company>(company);
            _repository.Company.CreateCompany(maptocompany);
            await _repository.SaveAsync();

            var companyToReturn = _mapper.Map<CompanyDTO>(maptocompany);
            return companyToReturn;

        }

        public async Task<(IEnumerable<CompanyDTO> companies, string Ids)> CreateCompanyByBulkAsync(IEnumerable<CompanyForCreationDto> companyCollection)
        {
            if (companyCollection is null)
            {
                throw new CompanyCollectionBadRequest();
            }
            var toCompanies =  _mapper.Map<IEnumerable<Company>>(companyCollection);
            foreach(var company in toCompanies){
               _repository.Company.CreateCompany(company);
            }
            
            await _repository.SaveAsync();

            var newcompanies = _mapper.Map<IEnumerable<CompanyDTO>>(toCompanies);
            var Ids = string.Join (",", newcompanies.Select(c => c.Id));

            return (companies: newcompanies, Ids: Ids);

        }

        public async Task DeleteCompanyAsync(Guid companyId, bool trackChanges)
        {
            var companyInstance = await _repository.Company.GetCompanyAsync(companyId, trackChanges)
             ?? throw new CompanyNotFoundException(companyId);

            _repository.Company.DeleteCompany(companyInstance);
            _repository.Save();

        }

        public async Task<IEnumerable<CompanyDTO>> GetAllCompaniesAsync(CompanyParameters companyParameters, bool trackChanges)
        {
           try {

                var companies = await _repository.Company.GetAllCompaniesAsync(companyParameters, trackChanges);
                // var companiesDTO = companies.Select(company => new CompanyDTO(
                //     company.Id, company.Name ?? "", string.Join(' ', company.Address, company.Country))).ToList();
                var companiesDTO = _mapper.Map<IEnumerable<CompanyDTO>>(companies);
                Console.WriteLine("Hello", companies.Count());
                return companiesDTO;
           }
           catch (Exception ex) {

                _logger.LogError($"Something went wrong in the {nameof(GetAllCompaniesAsync)} service method {ex}");
                throw;
           }

        }

        public async Task<IEnumerable<CompanyDTO>> GetCompaniesByIdsAsync(IEnumerable<Guid> Ids, bool trackChanges)
        {
            if(Ids is null){
               throw new IdParametersBadRequestException();
            }
            var companies = await _repository.Company.GetByIdsAsync(Ids, trackChanges);
            if (Ids.Count() != companies.Count()){
                throw new CollectionByIdsBadRequestException();
            }

            var companiesToReturn = _mapper.Map<IEnumerable<CompanyDTO>>(companies);
            return companiesToReturn;

        }

        public async Task<CompanyDTO> GetCompanyAsync(Guid Id, bool trackChanges)
        {
            var company = await _repository.Company.GetCompanyAsync(Id, trackChanges)
                ?? throw new CompanyNotFoundException(Id);
            var companyDTO = _mapper.Map<CompanyDTO>(company);
            return companyDTO;
        }

        public async Task<(CompanyForUpdateDto companyToPatch, Company companyEntity)> GetCompanyForPatchAsync(Guid companyId, bool compTrackChanges)
        {
            var companyInstance = await _repository.Company.GetCompanyAsync(companyId, compTrackChanges)
            ?? throw new CompanyNotFoundException(companyId);
            
            var companyDto = _mapper.Map<CompanyForUpdateDto>(companyInstance);
            return (companyDto, companyInstance);

        }

        public async Task SaveChangesForPatch(CompanyForUpdateDto companyDto, Company companyInstance)
        {
            _mapper.Map(companyDto, companyInstance);
            await _repository.SaveAsync();

        }

        public async Task UpdateCompanyAsync(Guid companyid, CompanyForUpdateDto companyForUpdate, bool trackChanges)
        {
            var company = await _repository.Company.GetCompanyAsync(companyid, trackChanges) 
                ?? throw new CompanyNotFoundException(companyid);
            _mapper.Map(companyForUpdate, company);
            _repository.Save();
        }
    }
}