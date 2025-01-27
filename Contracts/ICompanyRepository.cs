using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface ICompanyRepository
{
    IEnumerable<Company> GetAllCompanies(bool trackChanges);
    Company GetCompany(Guid companyId, bool trackChanges);
    IEnumerable<Company> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
    
    void CreateCompany(Company company);
    void DeleteCompany(Company company);


    Task<IEnumerable<Company>> GetAllCompaniesAsync(CompanyParameters companyParameters, bool trackChanges);
    Task<Company> GetCompanyAsync(Guid companyId, bool trackChanges); 
    Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges); 

}
