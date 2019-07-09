using System.Collections.Generic;
using FocLab.Model.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace FocLab.Model
{
    public class FocLabDbContextFactory : IDesignTimeDbContextFactory<ChemistryDbContext>
    {
        public ChemistryDbContext CreateDbContext(string[] args)
        {
            return ChemistryDbContext.Create("Server=(localdb)\\mssqllocaldb;Database=aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }

    public class FakeConfiguration : IConfiguration
    {
        public string this[string key] { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new System.NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new System.NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            throw new System.NotImplementedException();
        }
    }
}
