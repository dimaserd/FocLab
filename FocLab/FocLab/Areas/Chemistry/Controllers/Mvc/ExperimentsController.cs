using System.Threading.Tasks;
using Croco.Core.Common.Models;
using FocLab.Areas.Chemistry.Controllers.Base;
using FocLab.Logic.EntityDtos;
using FocLab.Logic.Services;
using FocLab.Logic.Workers.ChemistryTaskExperiments;
using FocLab.Logic.Workers.ChemistryTasks;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Areas.Chemistry.Controllers.Mvc
{
    /// <inheritdoc />
    /// <summary>
    /// Контроллер содержащий методы для работы с экспериментами
    /// </summary>
    public class ExperimentsController : BaseChemistryCustomController
    {
        #region Методы Апи

        /// <summary>
        /// Выполнить эксперимент
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BaseApiResponse> Perform(PerformExperimentModel model)
        {
            return await ChemistryTaskExperimentsWorker.PerformExperimentAsync(model, MailSender);
        }
        #endregion

        private ChemistryTaskExperimentsWorker ChemistryTaskExperimentsWorker => new ChemistryTaskExperimentsWorker(ContextWrapper);
        
        private ChemistryTasksWorker ChemistryTasksWorker => new ChemistryTasksWorker(ContextWrapper);

        /// <summary>
        /// Получить эксперименты
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            var list = await ChemistryTaskExperimentsWorker.GetMyExperimentsAsync();

            return View(list);
        }

        /// <summary>
        /// Все эксперименты
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> All()
        {
            var model = await ChemistryTaskExperimentsWorker.GetAllExperimentsWithUsers();

            return View(model);
        }

        /// <summary>
        /// Один эксперимент
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Experiment(string id)
        {
            var experiment = await ChemistryTaskExperimentsWorker.GetExperimentDtoAsync(id);

            if (experiment == null)
            {
                return RedirectToAction("Index");
            }

            return View(experiment);
        }

        #region Создание
        /// <summary>
        /// Создание эксперимента к заданию
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Create(string id)
        {
            var task = await ChemistryTasksWorker.GetChemistryTaskByIdAsync(id);

            if (task == null)
            {
                return RedirectToAction("Index");
            }

            return View(task);
        }

        /// <summary>
        /// Создание метод Апи
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> Create(ChemistryTaskExperimentDto model)
        {
            var experimentCreateResult = await ChemistryTaskExperimentsWorker.CreateExperimentForTaskAsync(model);

            return Json(experimentCreateResult);
        }
        #endregion

        public ExperimentsController(ChemistryDbContext context, ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(context, userManager, signInManager)
        {
        }
    }
}