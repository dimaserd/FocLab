using Croco.Core.Abstractions;
using Croco.Core.Application;
using Croco.Core.Logic.Workers;
using FocLab.Logic.Implementations;
using FocLab.Model.Contexts;

namespace FocLab.Logic.Workers
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseChemistryWorker : BaseCrocoWorker
    {
        /// <summary>
        /// Контекст приложения химия
        /// </summary>
        protected ChemistryDbContext Context => ApplicationContextWrapper.DbContext;

        protected IUserContextWrapper<ChemistryDbContext> ApplicationContextWrapper { get; }

        protected FocLabWebApplication Application => CrocoApp.Application.As<FocLabWebApplication>();

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="contextWrapper"></param>
        public BaseChemistryWorker(ICrocoAmbientContext) : base(contextWrapper)
        {
            ApplicationContextWrapper = contextWrapper;
        }
    }
}