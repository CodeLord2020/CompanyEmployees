using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyEmployees.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder().
            setBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.json", optional: false).
            .Build();

            var builder = new DbContextOptionsBuilder<RepositoryContext>().
            UseSqlServer(configuration.GetConnectionString("sqlConnnection"));

            return new RepositoryContext(builder.Options);
        }

        
    }
}