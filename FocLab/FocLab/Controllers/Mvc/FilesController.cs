using System;
using System.IO;
using System.Threading.Tasks;
using Croco.Core.Application;
using Croco.Core.Common.Enumerations;
using Croco.Core.Model.Entities.Application;
using FocLab.Controllers.Base;
using FocLab.Logic.Implementations;
using FocLab.Logic.Services;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FocLab.Controllers.Mvc
{
    /// <inheritdoc />
    /// <summary>
    /// Контроллер отвечающий за работу с файлами
    /// </summary>
    public class FilesController : BaseController
    {
        public FilesController(ChemistryDbContext context, ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(context, userManager, signInManager)
        {
        }

        /// <summary>
        /// Возвращает файл по указанному идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetDbFileById(int id, ImageSizeType type = ImageSizeType.Original)
        {
            return await GetResizedLocalImageFileById(id, type);
        }

        /// <summary>
        /// Возвращает изображение в новом размере по указанному идентификатору и типу размера
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetResizedLocalImageFileById(int id, ImageSizeType type = ImageSizeType.Original)
        {
            var filePath = CrocoApp.Application.FileCopyWorker.GetResizedImageLocalPath(id, type);

            if (System.IO.File.Exists(filePath))
            {
                return File(System.IO.File.ReadAllBytes(filePath), FocLabWebApplication.GetMimeMapping(filePath), Path.GetFileName(filePath));
            }

            var file = await Context.DbFiles.FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                var copyResult = CrocoApp.Application.FileCopyWorker.MakeLocalFileCopy(file);

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

        private IActionResult ReturnDbFileOrNotFound(DbFileBase file)
        {
            if (file != null)
            {
                return File(file.Data, FocLabWebApplication.GetMimeMapping(file.FileName), file.FileName);
            }

            return HttpNotFound();
        }
        #endregion
    }
}