using System;
using System.Threading.Tasks;
using Croco.Core.Abstractions;
using Croco.Core.Common.Models;
using FocLab.Areas.Chemistry.Controllers.Base;
using FocLab.Logic.EntityDtos;
using FocLab.Logic.Models;
using FocLab.Logic.Services;
using FocLab.Logic.Workers.ChemistryMethods;
using FocLab.Logic.Workers.ChemistryTaskExperiments;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Areas.Chemistry.Controllers.Mvc
{

    /// <inheritdoc />
    /// <summary>
    /// Контроллер для работы с химическими заданиями
    /// </summary>
    [Area("Chemistry")]
    [Authorize]
    public class ChemistryController : BaseChemistryCustomController
    {
        private ChemistryMethodsWorker ChemistryMethodsWorker => new ChemistryMethodsWorker(ContextWrapper);

        private ChemistryTaskExperimentsWorker ChemistryTaskExperimentsWorker =>
            new ChemistryTaskExperimentsWorker(ContextWrapper);


        #region Методы
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin,SuperAdmin,Root")]
        public async Task<ActionResult> Methods()
        {
            var model = await ChemistryMethodsWorker.GetTaskMethodsAsync();

            return View(model);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> EditMethod(string id)
        {
            var method = await ChemistryMethodsWorker.GetMethodAsync(id);

            return View(method);
        }
        #endregion

        #region Эксперименты
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> LoadOrReloadFileForExperiment(ChemistryChangeFileForExperiment model)
        {
            try
            {
                var t = await ChemistryTaskExperimentsWorker.LoadOrReloadFileForExperimentAsync(model);

                return Json(t);
            }
            catch (Exception ex)
            {
                return Json(new BaseApiResponse(false, ex.Message));
            }
            
        }

        #region Удаление 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BaseApiResponse> RemoveExperiment(string id)
        {
            return await ChemistryTaskExperimentsWorker.RemoveExperimentAsync(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BaseApiResponse> CancelRemovingExperiment(string id)
        {
            return await ChemistryTaskExperimentsWorker.CancelRemovingExperimentAsync(id);
        }
        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BaseApiResponse> UpdateExperiment(ChemistryTaskExperimentDto model)
        {
            return await ChemistryTaskExperimentsWorker.UpdateExperimentAsync(model);
        }

        #endregion

        #region Документы

        /// <summary>
        /// Распечатать задание
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ActionResult> PrintTask(string id)
        {
            //var t = await ChemistryDocumentProcessor.TestById(id);

            //const string fileName = "PrintDocument.docx";

            //var filePath = HostingEnvironment.MapPath($"~/Files/{fileName}");

            //return File(System.IO.File.ReadAllBytes(filePath), MimeMapping.GetMimeMapping(filePath), fileName);

            return null;
        }
        #endregion

        public ChemistryController(ChemistryDbContext context, ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(context, userManager, signInManager)
        {
        }
    }
}