using Croco.Core.Data.Implementations.DbAudit.Models;
using Croco.Core.EventSourcing.Implementations.StatusLog.Models;
using Croco.Core.Model.Entities;
using Croco.Core.Model.Entities.Store;
using FocLab.Model.Abstractions;
using FocLab.Model.Entities;
using FocLab.Model.Entities.Chemistry;
using FocLab.Model.Entities.Tasker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Zoo.Core;

namespace FocLab.Model.Contexts
{
    /// <summary>
    /// Контекст базы данных для приложения Химия
    /// </summary>
    public class ChemistryDbContext : ApplicationDbContext, IStoreContext
    {
        public const string ServerConnection = "ServerConnection";

        public const string LocalConnection = "DefaultConnection";

#if DEBUG
        public static string ConnectionString => LocalConnection;

#else
        public static string ConnectionString => ServerConnection;
#endif

        public static ChemistryDbContext Create(IConfiguration configuration)
        {
            return Create(configuration.GetConnectionString(ConnectionString));
        }

        public static ChemistryDbContext Create(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ChemistryDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ChemistryDbContext(optionsBuilder.Options);
        }


        #region IStore
        public DbSet<RobotTask> RobotTasks { get; set; }

        public DbSet<LoggedUserInterfaceAction> LoggedUserInterfaceActions { get; set; }

        public DbSet<LoggedApplicationAction> LoggedApplicationActions { get; set; }

        public DbSet<AuditLog> AuditLogs { get; set; }

        public DbSet<IntegrationMessageLog> IntegrationMessageLogs { get; set; }

        public DbSet<IntegrationMessageStatusLog> IntegrationMessageStatusLogs { get; set; }

        public DbSet<WebAppRequestContextLog> WebAppRequestContextLogs { get; set; }
        #endregion

        public DbSet<DbFile> DbFiles { get; set; }

        public DbSet<ApplicationDbFileHistory> DbFileHistory { get; set; }

        public DbSet<ApplicationDayTask> DayTasks { get; set; }

        public DbSet<ApplicationDayTaskComment> DayTaskComments { get; set; }

        #region Химимя
        /// <summary>
        /// Химические задания
        /// </summary>
        public DbSet<ChemistryTask> ChemistryTasks { get; set; }

        /// <summary>
        /// Файлы связанные с химическим заданием
        /// </summary>
        public DbSet<ChemistryTaskDbFile> ChemistryTaskDbFiles { get; set; }

        /// <summary>
        /// Методы решения химических 
        /// </summary>
        public DbSet<ChemistryMethodFile> ChemistryMethodFiles { get; set; }

        /// <summary>
        /// Эксперименты 
        /// </summary>
        public DbSet<ChemistryTaskExperiment> ChemistryTaskExperiments { get; set; }

        /// <summary>
        /// Файлы для экспериментов
        /// </summary>
        public DbSet<ChemistryTaskExperimentFile> ChemistryTaskExperimentFiles { get; set; }

        /// <summary>
        /// Реагенты
        /// </summary>
        public DbSet<ChemistryReagent> ChemistryReagents { get; set; }

        /// <summary>
        /// Реагенты связанные с химическими заданиями
        /// </summary>
        public DbSet<ChemistryTaskReagent> ChemistryTaskReagents { get; set; }

        /// <summary>
        /// Задания на день
        /// </summary>
        public DbSet<ChemistryDayTask> ChemistryDayTasks { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            LoggedApplicationAction.OnModelCreating(builder);

            builder.Entity<AuditLog>().Property(x => x.Id).ValueGeneratedOnAdd();

            WebAppRequestContextLog.OnModelCreating(builder);

            ChemistryTaskExperimentFile.OnModelCreating(builder);
            ChemistryTaskReagent.OnModelCreating(builder);
            ChemistryTaskDbFile.OnModelCreating(builder);

            base.OnModelCreating(builder);
        }

        public ChemistryDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}