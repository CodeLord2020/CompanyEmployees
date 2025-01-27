using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository
{
    internal sealed class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateCompany(Company company)
        {
            Create(company);
        }

        public void DeleteCompany(Company company)
        {
           Delete(company);
        }

        public IEnumerable<Company> GetAllCompanies(bool trackChanges) =>
           FindAll(trackChanges).OrderBy(c => c.Name).ToList();

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync(CompanyParameters companyParameters, bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.Name).
            Search(companyParameters.SearchTerm).
            Sort(companyParameters.OrderBy).
            ToListAsync();
        }

        public IEnumerable<Company> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
            FindByCondition(c => ids.Contains(c.Id), trackChanges).ToList();

        public async Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            return await FindByCondition(c => ids.Contains(c.Id), trackChanges)
            .ToListAsync();
        }

        public Company GetCompany(Guid companyId, bool trackChanges) 
        {
            var company = FindByCondition(c => c.Id.Equals(companyId), trackChanges).SingleOrDefault() 
            ?? throw new CompanyNotFoundException(companyId);
            return company;
        }

        public async Task<Company> GetCompanyAsync(Guid companyId, bool trackChanges) 
        {
           var  company = await FindByCondition(c => c.Id.Equals(companyId), trackChanges)
                .SingleOrDefaultAsync() ?? 
                            throw new CompanyNotFoundException(companyId);
                            return company;
        }
    }
}
