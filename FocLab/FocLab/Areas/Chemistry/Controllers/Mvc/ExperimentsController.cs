using System.Threading.Tasks;
using FocLab.Areas.Chemistry.Controllers.Base;
using FocLab.Consts;
using FocLab.Logic.Services;
using FocLab.Logic.Workers.ChemistryTaskExperiments;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Areas.Chemistry.Controllers.Mvc
{
    /// <inheritdoc />
    /// <summary>
    /// Контроллер содержащий методы для работы с экспериментами
    /// </summary>
    [Area(AreaConsts.Chemistry)]
    public class ExperimentsController : BaseFocLabController
    {
        private ChemistryTaskExperimentsWorker ChemistryTaskExperimentsWorker => new ChemistryTaskExperimentsWorker(AmbientContext);

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
            var model = await ChemistryTaskExperimentsWorker.GetAllExperimentsAsync();

            return View(model);
        }

        /// <summary>
        /// Один эксперимент
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Experiment(string id)
        {
            var experiment = await ChemistryTaskExperimentsWorker.GetExperimentAsync(id);

            if (experiment == null)
            {
                return RedirectToAction("Index");
            }

            return View(experiment);
        }

        /// <summary>
        /// Создание эксперимента к заданию
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        public ExperimentsController(ChemistryDbContext context, ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(context, userManager, signInManager)
        {
        }
    }
}