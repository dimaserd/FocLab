using Doc.Logic.Abstractions;
using Doc.Logic.Entities;
using Novacode;
using System.Linq;

namespace Doc.Logic.Implementations
{
    public class NovacodeWordEngine : IWordProccessorEngine
    {
        public void Create(DocXDocumentObjectModel model)
        {
            var document = DocX.Load(model.DocumentTemplateFileName);

            foreach (var key in model.Replaces.Keys)
            {
                document.ReplaceText(key, model.Replaces[key] ?? "");
            }


            foreach (var table in model.Tables)
            {
                var realTable = table.GetTable(document);
                realTable.Design = TableDesign.TableGrid;

                var border = new Border { Size = BorderSize.one };

                realTable.SetBorder(TableBorderType.Bottom, border);
                realTable.SetBorder(TableBorderType.InsideH, border);
                realTable.SetBorder(TableBorderType.InsideV, border);
                realTable.SetBorder(TableBorderType.Left, border);
                realTable.SetBorder(TableBorderType.Right, border);
                realTable.SetBorder(TableBorderType.Top, border);


                // Insert a new Paragraph into the document.
                var p1 = document.Paragraphs.FirstOrDefault(x => x.Text == table.PlacingText);

                p1.InsertTableBeforeSelf(realTable);

                p1.ReplaceText(table.PlacingText, "");
                p1.Remove(true);
            }

            document.SaveAs(model.DocumentSaveFileName);
        }
    }
}
