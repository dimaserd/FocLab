using Microsoft.EntityFrameworkCore.Design;
using Tms.Model;

namespace FocLab.DbFactories
{
    public class TmsDbContextFactory : IDesignTimeDbContextFactory<TmsDbContext>
    {
        public TmsDbContext CreateDbContext(string[] args)
        {
            return MySqLiteFileDatabaseExtensions.Create<TmsDbContext>(opts => new TmsDbContext(opts), "tms");
        }
    }
}