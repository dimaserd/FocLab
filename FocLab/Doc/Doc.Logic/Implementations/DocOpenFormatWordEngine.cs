using Croco.Core.Application;
using Doc.Logic.Abstractions;
using Doc.Logic.Entities;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using System.IO;

namespace Doc.Logic.Implementations
{
    public class DocOpenFormatWordEngine : IWordProccessorEngine
    {
        

        public void Create(DocXDocumentObjectModel model)
        {
            using (WordprocessingDocument doc =
                    WordprocessingDocument.Open(model.DocumentFileName, true))
            {
                var body = doc.MainDocumentPart.Document.Body;

                var texts = FindTextsInElement(body);

                foreach (var text in texts)
                {
                    foreach (var toReplace in model.Replaces)
                    {
                        if (text.Text.Contains(toReplace.Key))
                        {
                            text.Text = text.Text.Replace(toReplace.Key, toReplace.Value);
                        }
                    }
                }

                var t = doc.SaveAs(model.DocumentSaveFileName);
                t.Dispose();
            }
        }

        public static void OpenAndAddTextToWordDocument(string filepath, string txt)
        {
            // Open a WordprocessingDocument for editing using the filepath.
            WordprocessingDocument wordprocessingDocument =
                WordprocessingDocument.Open(filepath, true);

            // Assign a reference to the existing document body.
            Body body = wordprocessingDocument.MainDocumentPart.Document.Body;

            // Add new text.
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text(txt));

            // Close the handle explicitly.
            wordprocessingDocument.Close();
        }

        private static List<Text> FindTextsInElement(OpenXmlElement elem)
        {
            var res = new List<Text>();

            if (elem is Text)
            {
                res.Add(elem as Text);
                return res;
            }


            foreach(var child in elem.ChildElements)
            {
                res.AddRange(FindTextsInElement(child));
            }

            return res;
        }
        
    }
}
