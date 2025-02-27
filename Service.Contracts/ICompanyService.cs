using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface ICompanyService
{
    Task<IEnumerable<CompanyDTO>> GetAllCompaniesAsync(CompanyParameters companyParameters, bool trackChanges);
    Task<CompanyDTO> GetCompanyAsync(Guid companyId, bool trackChanges);
    Task<CompanyDTO> CreateCompanyAsync(CompanyForCreationDto company);
    Task<IEnumerable<CompanyDTO>> GetCompaniesByIdsAsync(IEnumerable<Guid> Ids, bool trackChanges);
    Task<(IEnumerable<CompanyDTO> companies, string Ids)> CreateCompanyByBulkAsync(IEnumerable<CompanyForCreationDto> companyCollection);
    Task DeleteCompanyAsync(Guid companyId, bool trackChanges);
    Task UpdateCompanyAsync(Guid companyid, CompanyForUpdateDto companyForUpdate, bool trackChanges);
    Task<(CompanyForUpdateDto companyToPatch, Company companyEntity)> GetCompanyForPatchAsync(
        Guid companyId, bool compTrackChanges);
    Task SaveChangesForPatch(CompanyForUpdateDto companyDto, Company companyInstance);
}
