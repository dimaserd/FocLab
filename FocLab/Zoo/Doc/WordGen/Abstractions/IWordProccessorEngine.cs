using Doc.Logic.Entities;

namespace Zoo.Doc.WordGen.Abstractions
{
    public interface IWordProccessorEngine
    {
        void Create(DocXDocumentObjectModel model);
    }
}
