using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface ICompanyService
{
    Task<IEnumerable<CompanyDTO>> GetAllCompanies(bool trackChanges);
    Task<CompanyDTO> GetCompany(Guid companyId, bool trackChanges);
    Task<CompanyDTO> CreateCompany(CompanyForCreationDto company);
    Task<IEnumerable<CompanyDTO>> GetCompaniesById(IEnumerable<Guid> Ids, bool trackChanges);
    Task<(IEnumerable<CompanyDTO> companies, string Ids)> CreateCompanyByBulk(IEnumerable<CompanyForCreationDto> companyCollection);
    Task DeleteCompany(Guid companyId, bool trackChanges);
    Task UpdateCompany(Guid companyid, CompanyForUpdateDto companyForUpdate, bool trackChanges);

}
