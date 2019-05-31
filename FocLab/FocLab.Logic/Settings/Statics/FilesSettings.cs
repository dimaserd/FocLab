using System.Collections.Generic;
using System.IO;
using System.Linq;
using Croco.Core.Abstractions;

namespace FocLab.Logic.Settings.Statics
{
    public static class FilesSettings
    {
        #region Константы

        public static readonly string[] ImageExtensions = {
            ".jpg", ".jpeg", ".tiff", ".png", ".bmp", ".gif", ".webp",
        };

        static readonly string[] DocumentExtensions = {
            ".docx", ".doc", ".xls", ".xlsx", ".txt",  ".ppt", ".pptx", ".pdf"
        };

        static readonly string[] VideoExtensions = {
            ".mp4", ".3gp", ".mkv", ".webm", ".ogg", ".avi"
        };

        private static readonly string[] MusicExtensions = {
            ".mp3", ".flac", ".ape", ".ogg", ".waw", ".ac3", ".wma", ".m4a",
        };

        public static readonly string[] ArchiveExtensions = {
            ".rar", ".zip", ".7z", ".tar", ".gzip", ".gz", ".jar"
        };

        private static readonly string[] HtmlDocumentFiles = {
            ".css", ".less", ".sass", ".html", ".otf", ".svg",
            ".js", ".map", ".md"
        };
        #endregion

        #region Публичные Методы
        public static bool IsImage(this IFileData file)
        {
            var ext = GetExtension(file);

            return ImageExtensions.Any(x => x == ext);
        }

        public static bool IsGoodFile(this IFileData file)
        {
            return IsGoodFile(file.FileName);
        }

        public static bool IsGoodFile(string fileName)
        {
            var ext = Path.GetExtension(fileName)?.ToLower();

            var allExtensions = GetExtensions();

            return allExtensions.Any(x => x == ext);
        }


        public static List<string> GetExtensions()
        {
            var allExtensions = new List<string>();

            allExtensions.AddRange(ImageExtensions);
            allExtensions.AddRange(DocumentExtensions);
            allExtensions.AddRange(MusicExtensions);
            allExtensions.AddRange(ArchiveExtensions);
            allExtensions.AddRange(VideoExtensions);
            allExtensions.AddRange(HtmlDocumentFiles);

            return allExtensions;
        }
        #endregion

        #region Вспомогательные методы
        public static string GetExtension(IFileData file)
        {
            return Path.GetExtension(file.FileName)?.ToLower();
        }
        #endregion
    }
}
