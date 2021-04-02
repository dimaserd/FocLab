using Croco.Core.Contract.Files;
using Croco.Core.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace FocLab.App.Logic.Extensions
{
    /// <summary>
    /// Расширения файлов
    /// </summary>
    public static class FileExtensions
    {
        /// <summary>
        /// Переложить файл из платформы AspNetCore для работы с библиотекой NetCroco.Core
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static IFileData ToFileData(this IFormFile file)
        {
            using var ms = new MemoryStream();
            file.CopyTo(ms);
            var fileBytes = ms.ToArray();

            return new CrocoFileData
            {
                FileName = file.FileName,
                FileData = fileBytes
            };
        }
    }
}