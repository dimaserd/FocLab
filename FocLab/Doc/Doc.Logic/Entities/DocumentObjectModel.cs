using System.Collections.Generic;

namespace Doc.Logic.Entities
{
    public class DocXDocumentObjectModel
    {
        /// <summary>
        /// Шаблон документа для преобразования
        /// </summary>
        public string DocumentTemplateFileName { get; set; }

        public string DocumentSaveFileName { get; set; }

        public Dictionary<string, string> Replaces { get; set; }

        public Dictionary<string, string> ToReplaceImages { get; set; }

        public List<DocumentTable> Tables { get; set; }
    }
}
