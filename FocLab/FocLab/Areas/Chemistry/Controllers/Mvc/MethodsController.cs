using FocLab.Areas.Chemistry.Controllers.Base;
using FocLab.Consts;
using FocLab.Logic.Services;
using FocLab.Logic.Workers.ChemistryMethods;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FocLab.Areas.Chemistry.Controllers.Mvc
{
    [Area(AreaConsts.Chemistry)]
    public class MethodsController : BaseFocLabController
    {
        public MethodsController(ChemistryDbContext context, ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(context, userManager, signInManager)
        {
        }

        private ChemistryMethodsWorker ChemistryMethodsWorker => new ChemistryMethodsWorker(AmbientContext);


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin,SuperAdmin,Root")]
        public async Task<ActionResult> Index()
        {
            var model = await ChemistryMethodsWorker.GetMethodsAsync();

            return View(model);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Edit(string id)
        {
            var method = await ChemistryMethodsWorker.GetMethodAsync(id);

            return View(method);
        }
    }
}