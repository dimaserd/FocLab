using NewFocLab.Model.Entities.Chemistry;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using NewFocLab.Model.External;

namespace NewFocLab.Model
{
    public class FocLabDbContext : DbContext
    {
        public FocLabDbContext([NotNull] DbContextOptions<FocLabDbContext> options) : base(options)
        {
        }

        public DbSet<FocLabDbFile> Files { get; set; }

        public DbSet<FocLabUser> Users { get; set; }

        #region Химимя
        /// <summary>
        /// Химические задания
        /// </summary>
        public DbSet<ChemistryTask> Tasks { get; set; }

        /// <summary>
        /// Файлы связанные с химическим заданием
        /// </summary>
        public DbSet<ChemistryTaskDbFile> TaskDbFiles { get; set; }

        /// <summary>
        /// Методы решения химических 
        /// </summary>
        public DbSet<ChemistryMethodFile> MethodFiles { get; set; }

        /// <summary>
        /// Эксперименты 
        /// </summary>
        public DbSet<ChemistryTaskExperiment> TaskExperiments { get; set; }

        /// <summary>
        /// Файлы для экспериментов
        /// </summary>
        public DbSet<ChemistryTaskExperimentFile> TaskExperimentFiles { get; set; }

        /// <summary>
        /// Реагенты
        /// </summary>
        public DbSet<ChemistryReagent> Reagents { get; set; }

        /// <summary>
        /// Реагенты связанные с химическими заданиями
        /// </summary>
        public DbSet<ChemistryTaskReagent> TaskReagents { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            FocLabDbFile.OnModelCreating(builder);
            FocLabUser.OnModelCreating(builder);

            ChemistryTaskExperimentFile.OnModelCreating(builder);
            ChemistryTaskReagent.OnModelCreating(builder);
            ChemistryTaskDbFile.OnModelCreating(builder);

            base.OnModelCreating(builder);
        }
    }
}