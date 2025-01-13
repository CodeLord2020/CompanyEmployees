using Contracts;
using Entities.Exceptions;
using Entities.Models;

namespace Repository
{
    internal sealed class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateCompany(Company company)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Company> GetAllCompanies(bool trackChanges) =>
           FindAll(trackChanges).OrderBy(c => c.Name).ToList();
        

        public IEnumerable<Company> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Company GetCompany(Guid companyId, bool trackChanges) 
        {
            var company = FindByCondition(c => c.Id.Equals(companyId), trackChanges).SingleOrDefault();
            if (company == null)
            {
                throw new CompanyNotFoundException(companyId);
            }
            return company;
        }
        
        // =>
        //     FindByCondition(c => c.Id.Equals(companyId), trackChanges)
        //     .SingleOrDefault();
    }
}
