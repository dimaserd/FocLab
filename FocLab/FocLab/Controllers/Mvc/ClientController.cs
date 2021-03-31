using System.Threading.Tasks;
using Clt.Logic.Services.Users;
using Croco.Core.Contract;
using FocLab.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

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
            ClientWorker clientWorker) : base(requestContextAccessor)
        {
            ClientWorker = clientWorker;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var response = await ClientWorker.GetClientFromAuthorizationAsync();

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