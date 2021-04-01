using Croco.Core.Contract;
using Croco.Core.Contract.Application;
using Croco.Core.Logic.Services;
using NewFocLab.Model;

namespace FocLab.Logic.Implementations
{
    public class FocLabWorker : BaseCrocoService<FocLabDbContext>
    {
        public FocLabWorker(ICrocoAmbientContextAccessor context, ICrocoApplication application) 
            : base(context, application)
        {
        }
    }
}