using System;
using System.Threading.Tasks;
using Croco.Core.Application;
using Doc.Logic.Models;
using Doc.Logic.Workers;
using FocLab.Areas.Chemistry.Controllers.Base;
using FocLab.Consts;
using FocLab.Logic.Services;
using FocLab.Logic.Workers.ChemistryTaskExperiments;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Areas.Chemistry.Controllers.Mvc
{
    /// <inheritdoc />
    /// <summary>
    /// Контроллер содержащий методы для работы с экспериментами
    /// </summary>
    [Area(AreaConsts.Chemistry), Authorize]
    public class ExperimentsController : BaseFocLabController
    {
        private ChemistryTaskExperimentsWorker ChemistryTaskExperimentsWorker => new ChemistryTaskExperimentsWorker(AmbientContext);

        ChemistryExperimentDocumentProccessor DocumentProccessor => new ChemistryExperimentDocumentProccessor(AmbientContext);

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

        public async Task<FileResult> Print(string id)
        {
            var fileName = $"Experiment.docx";

            var filePath = CrocoApp.Application.MapPath($"~/wwwroot/Docs/{fileName}");

            var t = await DocumentProccessor.RanderExperimentByIdAsync(new RenderChemistryExperimentDocument
            {
                ExperimentId = id,
                DocSaveFileName = filePath
            });

            if (!t.IsSucceeded)
            {
                throw new ApplicationException(t.Message);
            }

            return PhysicalFileWithMimeType(filePath, fileName);
        }

        public ExperimentsController(ChemistryDbContext context, ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(context, userManager, signInManager)
        {
        }
    }
}