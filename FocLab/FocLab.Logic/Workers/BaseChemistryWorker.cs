using Croco.Core.Abstractions.ContextWrappers;
using Croco.Core.Logic.Workers;
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

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="contextWrapper"></param>
        public BaseChemistryWorker(IUserContextWrapper<ChemistryDbContext> contextWrapper) : base(contextWrapper)
        {
            ApplicationContextWrapper = contextWrapper;
        }
    }
}