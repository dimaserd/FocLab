using Croco.Core.Contract.Models;
using Zoo.Doc.WordGen.Implementations;
using Zoo.Doc.WordGen.Models;
using Zoo.Doc.WordGen.Workers;

namespace Doc.Logic.Workers
{
    public class DocumentProccessorBase
    {
        public BaseApiResponse RenderDocument(DocXDocumentObjectModel docModel)
        {
            var proccessor = new WordDocumentProcessor(new WordDocumentProcessorOptions
            {
                Engine = new DocOpenFormatWordEngine()
            });

            return proccessor.RenderDocument(docModel);
        }
    }
}