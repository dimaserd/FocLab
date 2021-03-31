using Croco.Core.Contract;
using Croco.Core.Contract.Application;
using Croco.Core.Logic.Services;
using NewFocLab.Model;

namespace NewFocLab.Logic.Services
{
    public class FocLabService : BaseCrocoService<FocLabDbContext>
    {
        public FocLabService(ICrocoAmbientContextAccessor context, ICrocoApplication application)
            : base(context, application)
        {
        }
    }
}