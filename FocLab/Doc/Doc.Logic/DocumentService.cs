using Croco.Core.Abstractions;
using Croco.Core.Abstractions.Files;
using Doc.Contract.Services;
using System.IO;

namespace Doc.Logic
{
    public class DocumentService : IDocumentService
    {
        ICrocoAmbientContext AmbientContext { get; }

        public DocumentService(ICrocoAmbientContext ambientContext)
        {
            AmbientContext = ambientContext;
        }

        

        private class FileData : IFileData
        {
            public FileData(string fileName, string filePath)
            {
                FileName = fileName;
                Data = File.ReadAllBytes(filePath);
            }

            public string FileName { get; set; }
            public byte[] Data { get; set; }
        }
    }
}
