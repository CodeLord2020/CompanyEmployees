using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface ICompanyService
{
    Task <IEnumerable<CompanyDTO>> GetAllCompaniesAsync(bool trackChanges);
    Task <CompanyDTO> GetCompanyAsync(Guid companyId, bool trackChanges);
    Task <CompanyDTO> CreateCompanyAsync(CompanyForCreationDto company);
    Task <IEnumerable<CompanyDTO>> GetCompaniesByIdAsync(IEnumerable<Guid> Ids, bool trackChanges);
    Task <(IEnumerable<CompanyDTO> companies, string Ids)> CreateCompanyByBulkAsync(IEnumerable<CompanyForCreationDto> companyCollection);
    void DeleteCompanyAsync(Guid companyId, bool trackChanges);
    void UpdateCompanyAsync(Guid companyid, CompanyForUpdateDto companyForUpdate, bool trackChanges);

}
