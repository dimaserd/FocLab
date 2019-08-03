using System.Collections.Generic;

namespace Zoo.Doc.WordGen.Models
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
    }
}
