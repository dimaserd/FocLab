using FocLab.Model.Entities;
using FocLab.Model.Entities.Chemistry;
using Microsoft.EntityFrameworkCore;
using NewFocLab.Model.External;
using System.Diagnostics.CodeAnalysis;

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
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            ChemistryTaskExperimentFile.OnModelCreating(builder);
            ChemistryTaskReagent.OnModelCreating(builder);
            ChemistryTaskDbFile.OnModelCreating(builder);

            base.OnModelCreating(builder);
        }
    }
}