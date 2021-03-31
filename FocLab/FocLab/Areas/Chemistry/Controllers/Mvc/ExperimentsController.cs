using System;
using System.Threading.Tasks;
using Croco.Core.Application;
using Croco.Core.Contract;
using FocLab.Consts;
using FocLab.Controllers.Base;
using FocLab.Implementations.Doc;
using FocLab.Logic.Models.Doc;
using FocLab.Logic.Models.Experiments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewFocLab.Logic.Services.ChemistryTaskExperiments;
using Zoo.GenericUserInterface.Services;

namespace FocLab.Areas.Chemistry.Controllers.Mvc
{
    /// <inheritdoc />
    /// <summary>
    /// Контроллер содержащий методы для работы с экспериментами
    /// </summary>
    [Area(AreaConsts.Chemistry), Authorize]
    public class ExperimentsController : BaseController
    {
        ChemistryTaskExperimentsWorker ChemistryTaskExperimentsWorker { get; }

        ChemistryExperimentDocumentProccessor DocumentProccessor { get; }
        GenericUserInterfaceBag InterfacesBag { get; }

        public ExperimentsController(ChemistryTaskExperimentsWorker chemistryTaskExperimentsWorker,
            ChemistryExperimentDocumentProccessor documentProccessor,
            GenericUserInterfaceBag interfacesBag,
            ICrocoRequestContextAccessor requestContextAccessor) : base(requestContextAccessor)
        {
            ChemistryTaskExperimentsWorker = chemistryTaskExperimentsWorker;
            DocumentProccessor = documentProccessor;
            InterfacesBag = interfacesBag;
        }

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
        public async Task<ActionResult> Create()
        {
            var model = await InterfacesBag.GetDefaultInterface<CreateExperiment>();
            model.Interface.Prefix = "create.";
            return View(model);
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
    }
}