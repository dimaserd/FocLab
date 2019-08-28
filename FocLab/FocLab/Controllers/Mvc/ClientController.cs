using System.Threading.Tasks;
using FocLab.Controllers.Base;
using FocLab.Logic.Services;
using FocLab.Logic.Workers.Users;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Controllers.Mvc
{
    /// <inheritdoc />
    /// <summary>
    /// Контроллер предоставляющий методы для обновления данных клиента
    /// </summary>
    public class ClientController : BaseController
    {
        public ClientController(ChemistryDbContext context, ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(context, userManager, signInManager)
        {
        }

        private ClientWorker Worker => new ClientWorker(AmbientContext, x => SignInManager.SignInAsync(x, true));

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var response = await Worker.GetUserAsync();

            if (!response.IsSucceeded)
            {
                return HttpNotFound();
            }

            var model = response.ResponseObject;
            return View(model);
        }

        public async Task<IActionResult> Details(string id)
        {
            var resp = await Worker.GetClientByIdAsync(id);

            return View(resp);
        }
    }
}