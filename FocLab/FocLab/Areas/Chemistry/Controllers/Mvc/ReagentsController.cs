using System.Threading.Tasks;
using FocLab.Consts;
using FocLab.Logic.Models.Reagents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewFocLab.Logic.Services.ChemistryReagents;
using Zoo.GenericUserInterface.Services;

namespace FocLab.Areas.Chemistry.Controllers.Mvc
{
    /// <inheritdoc />
    /// <summary>
    /// Контроллер для реагентов
    /// </summary>
    [Area(AreaConsts.Chemistry), Authorize]
    public class ReagentsController : Controller
    {
        private ChemistryReagentsWorker ChemistryReagentsWorker { get; }
        private GenericUserInterfaceBag InterfaceBag { get; }

        public ReagentsController(
            ChemistryReagentsWorker chemistryReagentsWorker,
            GenericUserInterfaceBag interfaceBag)
        {
            ChemistryReagentsWorker = chemistryReagentsWorker;
            InterfaceBag = interfaceBag;
        }

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

            var interfaceDefinition = await InterfaceBag.GetDefaultInterface<ChemistryReagentNameAndIdModel>();
            interfaceDefinition.Interface.Prefix = "update.";

            ViewData["interfaceDefinition"] = interfaceDefinition;

            return View(model);
        }


        /// <summary>
        /// Создание
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create()
        {
            var model = await InterfaceBag.GetDefaultInterface<ChemistryReagentNameAndIdModel>();
            model.Interface.Prefix = "create.";
            return View(model);
        }
    }
}