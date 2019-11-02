using Croco.Core.Models;
using Doc.Logic.Entities;
using System;
using Zoo.Doc.WordGen.Abstractions;

namespace Zoo.Doc.WordGen.Workers
{
    public class WordDocumentProcessorOptions
    {
        public IWordProccessorEngine Engine { get; set; }
    }

    public class WordDocumentProcessor
    {
        public WordDocumentProcessor(WordDocumentProcessorOptions options)
        {
            Engine = options.Engine;
        }

        IWordProccessorEngine Engine { get; }

        public BaseApiResponse RenderDocument(DocXDocumentObjectModel model)
        {
            try
            {
                Engine.Create(model);
                return new BaseApiResponse(true, "Документ создан");
            }
            catch (Exception ex)
            {
                return new BaseApiResponse(ex);
            }
        }
    }

}
