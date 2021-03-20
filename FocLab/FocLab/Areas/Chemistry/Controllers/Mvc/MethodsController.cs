using Croco.Core.Contract;
using FocLab.Consts;
using FocLab.Controllers.Base;
using FocLab.Logic.Workers.ChemistryMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FocLab.Areas.Chemistry.Controllers.Mvc
{
    [Area(AreaConsts.Chemistry), Authorize]
    public class MethodsController : BaseController
    {
        private ChemistryMethodsWorker ChemistryMethodsWorker { get; }

        public MethodsController(ChemistryMethodsWorker chemistryMethodsWorker, 
            ICrocoRequestContextAccessor requestContextAccessor) : base(requestContextAccessor)
        {
            ChemistryMethodsWorker = chemistryMethodsWorker;
        }

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