using System;
using System.IO;
using System.Threading.Tasks;
using Croco.Core.Common.Enumerations;
using Croco.Core.Contract;
using Croco.Core.Contract.Files;
using Croco.Core.Logic.Files.Abstractions;
using Croco.Core.Model.Entities.Application;
using FocLab.Controllers.Base;
using FocLab.Logic.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace FocLab.Controllers.Mvc
{
    /// <inheritdoc />
    /// <summary>
    /// Контроллер отвечающий за работу с файлами
    /// </summary>
    public class FilesController : BaseController
    {
        IDbFileManager FileManager { get; }
        ICrocoFileCopyWorker FileCopyWorker { get; }

        public FilesController(IDbFileManager fileManager,
            ICrocoFileCopyWorker fileCopyWorker,
            ICrocoRequestContextAccessor requestContextAccessor,
            IActionContextAccessor contextAccessor) : base(requestContextAccessor, contextAccessor)
        {
            FileManager = fileManager;
            FileCopyWorker = fileCopyWorker;
        }

        /// <summary>
        /// Возвращает файл по указанному идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Task<IActionResult> GetDbFileById(int id, ImageSizeType type = ImageSizeType.Original)
        {
            return GetResizedLocalImageFileById(id, type);
        }

        /// <summary>
        /// Возвращает изображение в новом размере по указанному идентификатору и типу размера
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetResizedLocalImageFileById(int id, ImageSizeType type = ImageSizeType.Original)
        {
            var filePath = FileCopyWorker.GetResizedImageLocalPath(id, type);

            if (System.IO.File.Exists(filePath))
            {
                return File(System.IO.File.ReadAllBytes(filePath), FocLabWebApplication.GetMimeMapping(filePath), Path.GetFileName(filePath));
            }

            var file = await FileManager.GetFileDataById(id);

            try
            {
                var copyResult = await FileCopyWorker.MakeLocalFileCopy(new DbFileIntId 
                {
                    Id = id,
                    FileData = file.FileData,
                    FileName = file.FileName
                });

                if (!copyResult.IsSucceeded && copyResult.Message == "webp")
                {
                    return ReturnDbFileOrNotFound(file);
                }
            }
            catch (Exception)
            {
                return ReturnDbFileOrNotFound(file);
            }

            return await GetResizedLocalImageFileById(id, type);
        }

        

        #region Вспомогательные методы

        private IActionResult ReturnDbFileOrNotFound(IFileData file)
        {
            if (file != null)
            {
                return File(file.FileData, FocLabWebApplication.GetMimeMapping(file.FileName), file.FileName);
            }

            return HttpNotFound();
        }
        #endregion
    }
}