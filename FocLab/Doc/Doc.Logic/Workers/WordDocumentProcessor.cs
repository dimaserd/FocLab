using Doc.Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xceed.Words.NET;

namespace Doc.Logic.Workers
{
    public static class WordDocumentProcessor
    {

        public static void ProccessReplacing(string sourceFileName, string destFileName, Dictionary<string, string> replacesDictionary)
        {
            var document = DocX.Load(sourceFileName);

            foreach (var key in replacesDictionary.Keys)
            {
                document.ReplaceText(key, replacesDictionary[key]);
            }

            document.SaveAs(destFileName);
        }

        public static Tuple<bool, string> RenderDocument(DocXDocumentObjectModel model)
        {
            var document = DocX.Load(model.DocumentFileName);

            foreach (var key in model.Replaces.Keys)
            {
                document.ReplaceText(key, model.Replaces[key] ?? "");
            }

            foreach (var key in model.ToReplaceImages.Keys)
            {
                var myImageFullPath = model.ToReplaceImages[key];

                // Add an image into the document.    
                var image = document.AddImage(myImageFullPath);

                // Create a picture (A custom view of an Image).
                var picture = image.CreatePicture();


                var koef = picture.Width / (double)document.PageWidth;

                picture.Width = (int)(picture.Width / koef);
                picture.Height = (int)(picture.Height / koef);

                // Insert a new Paragraph into the document.
                var p1 = document.Paragraphs.FirstOrDefault(x => x.Text == key);

                p1.AppendPicture(picture);

                p1.ReplaceText(key, "");
            }

            foreach (var table in model.Tables)
            {
                var realTable = table.GetTable(document);

                var border = new Border { Size = BorderSize.one };

                realTable.SetBorder(TableBorderType.Bottom, border);
                realTable.SetBorder(TableBorderType.InsideH, border);
                realTable.SetBorder(TableBorderType.InsideV, border);
                realTable.SetBorder(TableBorderType.Left, border);
                realTable.SetBorder(TableBorderType.Right, border);
                realTable.SetBorder(TableBorderType.Top, border);


                // Insert a new Paragraph into the document.
                var p1 = document.Paragraphs.FirstOrDefault(x => x.Text == table.PlacingText);


                p1.ReplaceText(table.PlacingText, "");

                p1.InsertTableAfterSelf(realTable);

                p1.Remove(false);
            }

            document.SaveAs(model.DocumentSaveFileName);

            return new Tuple<bool, string>(true, "Документ создан");
        }
    }

}
