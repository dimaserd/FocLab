using Croco.Core.Contract;
using Croco.Core.Contract.Application;
using Croco.Core.Logic.Services;
using FocLab.Model;

namespace FocLab.Logic.Services
{
    public class FocLabService : BaseCrocoService<FocLabDbContext>
    {
        public FocLabService(ICrocoAmbientContextAccessor context, ICrocoApplication application)
            : base(context, application)
        {
        }
    }
}