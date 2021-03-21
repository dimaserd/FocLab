using Croco.Core.EventSourcing.Implementations.StatusLog.Models;
using Croco.Core.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace FocLab.Model.Contexts
{
    public class CrocoInternalDbContext : DbContext
    {
        public DbSet<RobotTask> RobotTasks { get; set; }

        public DbSet<IntegrationMessageLog> IntegrationMessageLogs { get; set; }

        public DbSet<IntegrationMessageStatusLog> IntegrationMessageStatusLogs { get; set; }
    }
}