using Croco.Core.Contract;
using Croco.Core.Contract.Application;
using Croco.Core.Contract.Models;
using FocLab.Logic.Implementations;
using Zoo.Doc.WordGen.Implementations;
using Zoo.Doc.WordGen.Models;
using Zoo.Doc.WordGen.Workers;

namespace Doc.Logic.Workers
{
    public class DocumentProccessorBase : FocLabWorker
    {
        public DocumentProccessorBase(ICrocoAmbientContextAccessor context, ICrocoApplication application)
            : base(context, application)
        {
        }

        public static BaseApiResponse RenderDocument(DocXDocumentObjectModel docModel)
        {
            var proccessor = new WordDocumentProcessor(new WordDocumentProcessorOptions
            {
                Engine = new DocOpenFormatWordEngine()
            });

            return proccessor.RenderDocument(docModel);
        }
    }
}