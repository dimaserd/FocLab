using System;
using System.Linq;
using System.Threading.Tasks;
using Croco.Core.Application;
using Croco.Core.Search;
using FocLab.Controllers.Base;
using FocLab.Logic.Services;
using FocLab.Logic.Workers;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public MyFilesController(ChemistryDbContext context, ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(context, userManager, signInManager)
        {
        }

        private DbFileWorker DbFileWorker => new DbFileWorker(ContextWrapper);




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
            var file = await Context.DbFiles.FirstOrDefaultAsync(x => x.Id == id);

            if(file == null)
            {
                return HttpNotFound();
            }

            return View(file);
        }

    }
}