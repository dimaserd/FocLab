using System;
using System.Threading.Tasks;
using Croco.Core.Common.Models;
using FocLab.Areas.Chemistry.Controllers.Base;
using FocLab.Consts;
using FocLab.Logic.EntityDtos;
using FocLab.Logic.Models;
using FocLab.Logic.Services;
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
    [Area(AreaConsts.Chemistry)]
    [Authorize]
    public class ChemistryController : BaseFocLabController
    {
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