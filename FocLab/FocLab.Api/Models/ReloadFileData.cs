using Microsoft.AspNetCore.Http;

namespace FocLab.Api.Models
{
    /// <summary>
    /// Перезагрузить содержимое файла
    /// </summary>
    public class ReloadFileData
    {
        /// <summary>
        /// Идентификатор файла
        /// </summary>
        public int FileId { get; set; }


        /// <summary>
        /// Данные в файле
        /// </summary>
        public IFormFile FileData { get; set; }
    }
}
