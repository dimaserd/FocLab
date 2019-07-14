using Doc.Logic.Abstractions;
using Doc.Logic.Entities;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            else
            {
                var dir = Path.GetDirectoryName(model.DocumentSaveFileName);

                Directory.CreateDirectory(dir);
            }

            using (var memStream = new MemoryStream())
            {
                var bytes = File.ReadAllBytes(model.DocumentTemplateFileName);

                memStream.Write(bytes, 0, bytes.Length);

                memStream.Seek(0, SeekOrigin.Begin);

                using (WordprocessingDocument doc =
                    WordprocessingDocument.Open(memStream, true))
                {
                    ProcessTextReplacing(doc, model);

                    foreach (var tableModel in model.Tables)
                    {
                        AddTable(doc, tableModel);
                    }

                    foreach(var image in model.ToReplaceImages)
                    {
                        DocImageInserter.InsertAPicture(doc, image);
                    }

                    var t = doc.SaveAs(model.DocumentSaveFileName);

                    t.Dispose();
                }
            }
        }

        
        private static void AddTable(WordprocessingDocument doc, DocumentTable tableModel)
        {
            var elem = doc.MainDocumentPart
                    .Document.Body
                    .ChildElements
                    .FirstOrDefault(x => x.InnerText == tableModel.PlacingText);

            if(elem == null)
            {
                throw new ApplicationException($"Не найден элемент с внутренним текстом '{tableModel.PlacingText}', " +
                    $"вместо которого нужно вставить таблицу.");
            }

            Table table = DocTableCreator.GetTable(tableModel);

            elem.RemoveAllChildren();
            elem.Append(table);
        }

        private static void ProcessTextReplacing(WordprocessingDocument doc, DocXDocumentObjectModel model)
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
