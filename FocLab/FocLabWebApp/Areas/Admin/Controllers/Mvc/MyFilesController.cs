using System.Threading.Tasks;
using Croco.Core.Contract;
using Croco.Core.Contract.Models.Search;
using FocLab.App.Logic.Workers;
using FocLab.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Areas.Admin.Controllers.Mvc
{
    /// <inheritdoc />
    /// <summary>
    /// Mvc-контроллер предоставляющий методы для работы с файлами
    /// </summary>
    [Authorize(Roles = "Admin,SuperAdmin,Root")]
    [Area("Admin")]
    public class MyFilesController : BaseController
    {
        private DbFileWorker DbFileWorker { get; }

        public MyFilesController(DbFileWorker dbFileWorker, 
            ICrocoRequestContextAccessor requestContextAccessor) : base(requestContextAccessor)
        {
            DbFileWorker = dbFileWorker;
        }

        /// <summary>
        /// Возвращает список файлов
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(GetListSearchModel model)
        {
            var viewModel = await DbFileWorker.GetFiles(model);

            return View(viewModel);
        }

        /// <summary>
        /// Возвращает список файлов (Частичное представление)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> FileListPartialIndex(GetListSearchModel model)
        {
            var viewModel = await DbFileWorker.GetFiles(model);

            return View("~/Areas/Admin/Views/MyFiles/Partials/FilesListPartial.cshtml", viewModel);
        }
    }
}