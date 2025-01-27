using Entities.Models;
using System.Linq.Dynamic.Core;
using Repository.Extensions.Utility;


namespace Repository.Extensions
{
    public static class RepositoryCompanyExtensions
    {


        public static IQueryable<Company> Search(this IQueryable<Company> employees,
            string searchTerm)
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                    return employees;

                var lowerCaseTerm = searchTerm.Trim().ToLower();
                return employees.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
            }
       public static IQueryable<Company> Sort(this IQueryable<Company> employees, string
            orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return employees.OrderBy(e => e.Name);
            }
            
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Employee>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                    return employees.OrderBy(e => e.Name);

            return employees.OrderBy(orderQuery);


        }
            

    }
}