using Croco.Core.Abstractions;
using Croco.Core.Abstractions.Settings;
using Croco.Core.Application;
using Croco.Core.Logic.Workers;
using FocLab.Logic.Implementations;
using FocLab.Model.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FocLab.Logic.Workers
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseChemistryWorker : BaseCrocoWorker
    {
        public DbContext DbContext => GetDbContext();


        public ChemistryDbContext Context => DbContext as ChemistryDbContext;

        protected T GetSetting<T>() where T : class, ICommonSetting<T>, new()
        {
            return CrocoApp.Application.SettingsFactory.GetSetting<T>();
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="contextWrapper"></param>
        public BaseChemistryWorker(ICrocoAmbientContext context) : base(context)
        {
        }
    }
}