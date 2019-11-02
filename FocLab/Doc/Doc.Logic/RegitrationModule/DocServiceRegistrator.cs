using Croco.Core.Abstractions;
using Croco.Core.Extensions;
using Doc.Contract.Services;

namespace Doc.Logic.RegistrationModule
{
    public static class DocServiceRegistrator
    {
        public static void Register(ICrocoApplication application)
        {
            application.RegisterService<IDocumentService, DocumentService>(x => new DocumentService(x));
        }
    }
}
