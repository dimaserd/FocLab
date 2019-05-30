using System;
using Croco.Core.Loggers;
using FocLab.Model.Entities.Chemistry;
using Microsoft.EntityFrameworkCore;

namespace FocLab.Model.Contexts
{
    /// <summary>
    /// Контекст базы данных для приложения Химия
    /// </summary>
    public class ChemistryDbContext : ApplicationDbContext
    {
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

        public ExceptionLogger GetLogger()
        {
            return new ExceptionLogger(this, () => DateTime.Now);
        }

        public ChemistryDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}