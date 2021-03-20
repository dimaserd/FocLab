using Croco.Core.Audit.Models;
using Croco.Core.EventSourcing.Implementations.StatusLog.Models;
using Croco.Core.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Zoo.Core;

namespace FocLab.Model.Abstractions
{
    public interface IStoreContext
    {
        DbSet<AuditLog> AuditLogs { get; set; }

        DbSet<IntegrationMessageLog> IntegrationMessageLogs { get; set; }

        DbSet<IntegrationMessageStatusLog> IntegrationMessageStatusLogs { get; set; }

        DbSet<RobotTask> RobotTasks { get; set; }

        DbSet<WebAppRequestContextLog> WebAppRequestContextLogs { get; set; }
    }
}