using System;
using Croco.Core.Loggers;
using Croco.Core.Model.Entities.Store;
using FocLab.Model.Entities;
using FocLab.Model.Entities.Chemistry;
using FocLab.Model.Entities.Tasker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FocLab.Model.Contexts
{
    /// <summary>
    /// Контекст базы данных для приложения Химия
    /// </summary>
    public class ChemistryDbContext : ApplicationDbContext
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


        #region Схема Store        
        public DbSet<LoggedUserInterfaceAction> UserInterfaceActions { get; set; }

        public DbSet<LoggedApplicationAction> ApplicationActions { get; set; }

        public DbSet<Snapshot> Snapshots { get; set; }
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