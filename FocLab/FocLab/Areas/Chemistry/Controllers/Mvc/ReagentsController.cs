using System.Threading.Tasks;
using FocLab.Areas.Chemistry.Controllers.Base;
using FocLab.Consts;
using FocLab.Logic.EntityDtos;
using FocLab.Logic.Services;
using FocLab.Logic.Workers.ChemistryReagents;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Areas.Chemistry.Controllers.Mvc
{
    /// <inheritdoc />
    /// <summary>
    /// Контроллер для реагентов
    /// </summary>
    [Area(AreaConsts.Chemistry)]
    public class ReagentsController : BaseFocLabController
    {
        private ChemistryReagentsWorker ChemistryReagentsWorker => new ChemistryReagentsWorker(ContextWrapper);

        #region методы Апи

        /// <summary>
        /// Создание реагента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> CreateReagent(ChemistryReagentDto model)
        {
            return Json(await ChemistryReagentsWorker.CreateReagentAsync(model));
        }

        /// <summary>
        /// Редактировние реагента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> EditReagent(ChemistryReagentDto model)
        {
            return Json(await ChemistryReagentsWorker.EditReagentAsync(model));
        }

        #region Реагенты к заданию

        /// <summary>
        /// Добавление реагента к заданию
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> AddTaskReagent(ChemistryTaskReagentDto model)
        {
            return Json(await ChemistryReagentsWorker.AddTaskReagentAsync(model));
        }

        /// <summary>
        /// Редактирование реагента к заданию
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> EditTaskReagent(ChemistryTaskReagentDto model)
        {
            return Json(await ChemistryReagentsWorker.EditTaskReagentAsync(model));
        }

        /// <summary>
        /// Удаление реагента
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> RemoveTaskReagent(ChemistryTaskReagentDto model)
        {
            return Json(await ChemistryReagentsWorker.RemoveTaskReagentAsync(model));
        }
        #endregion

        #endregion
        
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
            var model = await ChemistryReagentsWorker.GetReagentAsync(new ChemistryReagentDto { Id = id });

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