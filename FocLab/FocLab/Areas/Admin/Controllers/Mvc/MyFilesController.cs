using System.Threading.Tasks;
using Croco.Core.Contract;
using Croco.Core.Contract.Models.Search;
using FocLab.Controllers.Base;
using FocLab.Logic.Workers;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        private ChemistryDbContext DbContext { get; }

        public MyFilesController(DbFileWorker dbFileWorker, 
            ChemistryDbContext dbContext,
            ICrocoRequestContextAccessor requestContextAccessor) : base(requestContextAccessor)
        {
            DbFileWorker = dbFileWorker;
            DbContext = dbContext;
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


        /// <summary>
        /// Перезагрузить файл по указанному идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ReloadFile(int id)
        {
            var file = await DbContext.DbFiles.FirstOrDefaultAsync(x => x.Id == id);

            if(file == null)
            {
                return HttpNotFound();
            }

            return View(file);
        }
    }
}