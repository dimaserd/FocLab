using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using System.IO;
using Zoo.Doc.WordGen.Abstractions;
using Zoo.Doc.WordGen.Models;

namespace Zoo.Doc.WordGen.Implementations
{
    public class DocOpenFormatWordEngine : IWordProccessorEngine
    {
        /// <summary>
        /// УБрать из шаблона документа тексты, которые являются разбитыми по нескольким элементам
        /// </summary>
        /// <param name="model"></param>
        public void ProcessTemplate(DocXDocumentObjectModel model)
        {
            if (File.Exists(model.DocumentSaveFileName))
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
                    var body = doc.MainDocumentPart.Document.Body;

                    var paras = FindElemsInElement<Paragraph>(body);

                    foreach (var para in paras)
                    {
                        foreach (var toReplace in model.Replaces)
                        {
                            if (para.InnerText.Contains(toReplace.Key))
                            {
                                var pRun = para.GetFirstChild<Run>();

                                var fRunProp = pRun.GetFirstChild<RunProperties>().CloneNode(true);

                                var text = para.InnerText;

                                para.RemoveAllChildren<Run>();
                                para.AppendChild(new Run(fRunProp, new Text(text)));
                            }
                        }
                    }

                    var t = doc.SaveAs(model.DocumentTemplateFileName);

                    t.Dispose();
                }
            }
        }

        public void Create(DocXDocumentObjectModel model)
        {
            //Преподготовка шаблона удаление 
            ProcessTemplate(model);

            if (File.Exists(model.DocumentSaveFileName))
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

                using (WordprocessingDocument doc = WordprocessingDocument.Open(memStream, true))
                {
                    ProcessTextReplacing(doc, model);

                    foreach (var tableModel in model.Tables)
                    {
                        DocTableInserter.AddTable(doc, tableModel);
                    }

                    foreach (var image in model.ToReplaceImages)
                    {
                        DocImageInserter.InsertAPicture(doc, image);
                    }

                    var t = doc.SaveAs(model.DocumentSaveFileName);

                    t.Dispose();
                }
            }
        }

        

        private static void ProcessTextReplacing(WordprocessingDocument doc, DocXDocumentObjectModel model)
        {
            var body = doc.MainDocumentPart.Document.Body;

            var texts = FindElemsInElement<Text>(body);

            foreach (var text in texts)
            {
                foreach (var toReplace in model.Replaces)
                {
                    if (text.Text.Contains(toReplace.Key))
                    {
                        text.Text = text.Text.Replace(toReplace.Key, toReplace.Value ?? "");
                    }
                }
            }
        }

        private static List<TElem> FindElemsInElement<TElem>(OpenXmlElement elem) where TElem : OpenXmlElement
        {
            var res = new List<TElem>();

            if (elem is TElem)
            {
                res.Add(elem as TElem);
                return res;
            }

            foreach (var child in elem.ChildElements)
            {
                res.AddRange(FindElemsInElement<TElem>(child));
            }

            return res;
        }
    }
}
