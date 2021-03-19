using Croco.Core.Contract;
using Doc.Contract.Services;

namespace Doc.Logic
{
    public class DocumentService : IDocumentService
    {
        ICrocoAmbientContextAccessor AmbientContextAccessor { get; }

        public DocumentService(ICrocoAmbientContextAccessor ambientContext)
        {
            AmbientContextAccessor = ambientContext;
        }
    }
}