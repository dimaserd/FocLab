using Croco.Core.Audit.Models;
using Croco.Core.EventSourcing.Implementations.StatusLog.Models;
using Croco.Core.Model.Entities;
using FocLab.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace FocLab.Model.Contexts
{
    public class CrocoInternalDbContext : DbContext
    {
        public DbSet<RobotTask> RobotTasks { get; set; }

        public DbSet<IntegrationMessageLog> IntegrationMessageLogs { get; set; }

        public DbSet<IntegrationMessageStatusLog> IntegrationMessageStatusLogs { get; set; }

        public DbSet<AuditLog> AuditLogs { get; set; }

        public DbSet<DbFile> DbFiles { get; set; }

        public DbSet<ApplicationDbFileHistory> DbFileHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AuditLog>().Property(x => x.Id).ValueGeneratedOnAdd();

            base.OnModelCreating(builder);
        }
    }
}