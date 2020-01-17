using Croco.Core.Abstractions;
using Croco.Core.Logic.Workers;

namespace FocLab.Logic.Implementations
{
    public class FocLabWorker : BaseCrocoWorker<FocLabWebApplication>
    {
        public FocLabWorker(ICrocoAmbientContext context) : base(context)
        {
        }
    }
}