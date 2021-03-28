using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Tms.Model;

namespace FocLab.Model
{
    public class TmsDbContextFactory : IDesignTimeDbContextFactory<TmsDbContext>
    {
        const string LocalConnection = "Server=(localdb)\\mssqllocaldb;Database=FocLabTms;Trusted_Connection=True;MultipleActiveResultSets=true";

        public TmsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TmsDbContext>();
            optionsBuilder.UseSqlServer(LocalConnection);

            return new TmsDbContext(optionsBuilder.Options);
        }
    }
}