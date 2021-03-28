using Croco.Core.Contract;
using Croco.Core.Contract.Application;
using Croco.Core.Logic.Services;
using FocLab.Model.Contexts;

namespace FocLab.Logic.Implementations
{
    public class FocLabWorker : BaseCrocoService<ChemistryDbContext>
    {
        public FocLabWorker(ICrocoAmbientContextAccessor context, ICrocoApplication application) 
            : base(context, application)
        {
        }
    }
}