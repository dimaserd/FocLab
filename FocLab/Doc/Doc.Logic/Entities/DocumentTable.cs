using System.Collections.Generic;
using System.Linq;
using Xceed.Words.NET;

namespace Doc.Logic.Entities
{
    public class DocumentTable
    {
        public string PlacingText { get; set; }

        public List<string> Header { get; set; }

        public List<List<string>> Data { get; set; }

        private void CheckDataAndHeader()
        {
            Header.ForEach(x => { if (x == null) { x = ""; } });

            Data.ForEach(t => t.ForEach(x => { if (x == null) { x = ""; } }));
        }

        public Table GetTable(DocX docX)
        {
            CheckDataAndHeader();

            var realTable = docX.AddTable(Data.Count + 2, Header.Count);

            for (var i = 0; i < Header.Count; i++)
            {
                var paragraph = realTable.Rows[0].Cells[i].Paragraphs.First();
                paragraph.Append(Header[i]);
                paragraph.Alignment = Alignment.center;
                paragraph.Bold();
            }

            for (var i = 0; i < Data.Count; i++)
            {
                var row = realTable.Rows[i + 1];

                var dataElem = Data[i];

                for (var j = 0; j < dataElem.Count; j++)
                {
                    var paragraph = row.Cells[j].Paragraphs.First();

                    paragraph.Append(dataElem[j] != null ? dataElem[j] : "");
                }
            }

            return realTable;
        }
    }
}
