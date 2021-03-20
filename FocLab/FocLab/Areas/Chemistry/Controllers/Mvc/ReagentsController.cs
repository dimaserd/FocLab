using System.Threading.Tasks;
using Croco.Core.Contract;
using FocLab.Consts;
using FocLab.Controllers.Base;
using FocLab.Logic.Models.Reagents;
using FocLab.Logic.Workers.ChemistryReagents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zoo.GenericUserInterface.Services;

namespace FocLab.Areas.Chemistry.Controllers.Mvc
{
    /// <inheritdoc />
    /// <summary>
    /// Контроллер для реагентов
    /// </summary>
    [Area(AreaConsts.Chemistry), Authorize]
    public class ReagentsController : BaseController
    {
        private ChemistryReagentsWorker ChemistryReagentsWorker { get; }
        private GenericUserInterfaceBag InterfaceBag { get; }

        public ReagentsController(ICrocoRequestContextAccessor requestContextAccessor,
            ChemistryReagentsWorker chemistryReagentsWorker,
            GenericUserInterfaceBag interfaceBag) : base(requestContextAccessor)
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