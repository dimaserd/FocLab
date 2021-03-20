using System.Threading.Tasks;
using Croco.Core.Contract;
using FocLab.Controllers.Base;
using FocLab.Logic.Workers.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace FocLab.Controllers.Mvc
{
    /// <inheritdoc />
    /// <summary>
    /// Контроллер предоставляющий методы для обновления данных клиента
    /// </summary>
    public class ClientController : BaseController
    {
        ClientWorker ClientWorker { get; }

        public ClientController(ICrocoRequestContextAccessor requestContextAccessor,
            IActionContextAccessor actionContextAccessor,
            ClientWorker clientWorker) : base(requestContextAccessor, actionContextAccessor)
        {
            ClientWorker = clientWorker;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var response = await ClientWorker.GetUserAsync();

            if (!response.IsSucceeded)
            {
                return HttpNotFound();
            }

            var model = response.ResponseObject;
            return View(model);
        }

        public async Task<IActionResult> Details(string id)
        {
            var resp = await ClientWorker.GetClientByIdAsync(id);

            return View(resp);
        }
    }
}