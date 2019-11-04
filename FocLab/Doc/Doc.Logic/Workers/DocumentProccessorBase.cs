using Croco.Core.Abstractions;
using Croco.Core.Logic.Workers;
using Croco.Core.Models;
using Zoo.Doc.WordGen.Implementations;
using Zoo.Doc.WordGen.Models;
using Zoo.Doc.WordGen.Workers;

namespace Doc.Logic.Workers
{
    public class DocumentProccessorBase : BaseCrocoWorker
    {
        public DocumentProccessorBase(ICrocoAmbientContext context) : base(context)
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
