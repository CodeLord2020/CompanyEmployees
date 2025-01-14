using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface ICompanyService
{
    IEnumerable<CompanyDTO> GetAllCompanies(bool trackChanges);
    CompanyDTO GetCompany(Guid companyId, bool trackChanges);
    CompanyDTO CreateCompany(CompanyForCreationDto company);
    IEnumerable<CompanyDTO> GetCompaniesById(IEnumerable<Guid> Ids, bool trackChanges);

}
