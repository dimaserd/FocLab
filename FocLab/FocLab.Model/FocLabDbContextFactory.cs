using FocLab.Model.Contexts;
using Microsoft.EntityFrameworkCore.Design;

namespace FocLab.Model
{
    public class FocLabDbContextFactory : IDesignTimeDbContextFactory<ChemistryDbContext>
    {
        const string ServerConnection = "Server=ms-sql-9.in-solve.ru;Database=1gb_foclab-on-netcroco;Persist Security Info=True;Pooling=false;User ID=1gb_dimaserd;Password=add3835b723";

        const string LocalConnection = "Server=(localdb)\\MSSQLLocalDB;Database=aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502;Trusted_Connection=True;MultipleActiveResultSets=true";

        public ChemistryDbContext CreateDbContext(string[] args)
        {
            return ChemistryDbContext.Create(LocalConnection);
        }
    }
}