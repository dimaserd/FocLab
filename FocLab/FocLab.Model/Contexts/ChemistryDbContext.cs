using Croco.Core.Audit.Models;
using FocLab.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FocLab.Model.Contexts
{
    /// <summary>
    /// Контекст базы данных для приложения Химия
    /// </summary>
    public class ChemistryDbContext : ApplicationDbContext
    {
        public static ChemistryDbContext Create(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ChemistryDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ChemistryDbContext(optionsBuilder.Options);
        }


        #region IStore

        public DbSet<AuditLog> AuditLogs { get; set; }

        #endregion

        public DbSet<DbFile> DbFiles { get; set; }

        public DbSet<ApplicationDbFileHistory> DbFileHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AuditLog>().Property(x => x.Id).ValueGeneratedOnAdd();

            base.OnModelCreating(builder);
        }

        public ChemistryDbContext(DbContextOptions<ChemistryDbContext> options) : base(options)
        {
        }
    }
}