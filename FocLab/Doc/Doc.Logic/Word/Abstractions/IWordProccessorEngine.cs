using Croco.Core.Contract.Models;
using Doc.Logic.Word.Models;

namespace Doc.Logic.Word.Abstractions
{
    public interface IWordProccessorEngine
    {
        BaseApiResponse Create(DocXDocumentObjectModel model);
    }
}