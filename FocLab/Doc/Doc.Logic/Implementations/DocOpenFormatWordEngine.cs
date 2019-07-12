using Doc.Logic.Abstractions;
using Doc.Logic.Entities;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Packaging;
using System.IO;

namespace Doc.Logic.Implementations
{
    public class DocOpenFormatWordEngine : IWordProccessorEngine
    {
        

        public void Create(DocXDocumentObjectModel model)
        {
            if(File.Exists(model.DocumentSaveFileName))
            {
                File.Delete(model.DocumentSaveFileName);
            }
            File.Copy(model.DocumentFileName, model.DocumentSaveFileName);

            using (WordprocessingDocument doc =
                    WordprocessingDocument.Open(model.DocumentSaveFileName, true))
            {
                var document = doc.MainDocumentPart.Document;

                foreach (var text in document.Descendants<Text>()) // <<< Here
                {
                    foreach(var textToReplace in model.Replaces)
                    {
                        if (text.Text.Contains(textToReplace.Key))
                        {
                            text.Text = text.Text.Replace(textToReplace.Key, textToReplace.Value);
                        }
                    }
                }
            }
        }
    }
}
