using Croco.Core.Contract;
using Croco.Core.Contract.Application;
using Croco.Core.Logic.Workers;
using FocLab.Model.Contexts;

namespace FocLab.Logic.Implementations
{
    public class FocLabWorker : BaseCrocoWorker<ChemistryDbContext>
    {
        public FocLabWorker(ICrocoAmbientContextAccessor context, ICrocoApplication application) 
            : base(context, application)
        {
        }
    }
}