using System.Threading.Tasks;
using FocLab.Areas.Chemistry.Controllers.Base;
using FocLab.Consts;
using FocLab.Logic.Services;
using FocLab.Logic.Workers.ChemistryReagents;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Areas.Chemistry.Controllers.Mvc
{
    /// <inheritdoc />
    /// <summary>
    /// Контроллер для реагентов
    /// </summary>
    [Area(AreaConsts.Chemistry), Authorize]
    public class ReagentsController : BaseFocLabController
    {
        private ChemistryReagentsWorker ChemistryReagentsWorker => new ChemistryReagentsWorker(AmbientContext);

        /// <summary>
        /// Список реагентов
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var model = await ChemistryReagentsWorker.GetReagentsAsync();

            return View(model);
        }

        /// <summary>
        /// Подробнее
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Details(string id)
        {
            var model = await ChemistryReagentsWorker.GetReagentAsync(id);

            return View(model);
        }


        /// <summary>
        /// Создание
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        public ReagentsController(ChemistryDbContext context, ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(context, userManager, signInManager)
        {
        }
    }
}