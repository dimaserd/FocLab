using System.Threading.Tasks;
using Croco.Core.Common.Models;
using FocLab.Areas.Chemistry.Controllers.Base;
using FocLab.Logic.Models.ChemistryTasks;
using FocLab.Logic.Models.Tasks;
using FocLab.Logic.Services;
using FocLab.Logic.Workers.ChemistryTasks;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Areas.Chemistry.Controllers.Api
{
    /// <inheritdoc />
    /// <summary>
    /// Апи контроллер предоставляющий методы для работы с химическими заданиями
    /// </summary>
    [Route("Api/Chemistry/Tasks")]
    public class TaskApiController : BaseFocLabApiController
    {
        private AdminChemistryTasksWorker AdminChemistryTasksWorker => new AdminChemistryTasksWorker(ContextWrapper);

        private PerformerChemistryTasksWorker PerformerChemistryTasksWorker => new PerformerChemistryTasksWorker(ContextWrapper);

        /// <summary>
        /// Обновить поля задания как исполнитель
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Performer/Update")]
        public Task<BaseApiResponse> PerformerUpdate(UpdateTaskAsPerformer model)
        {
            return PerformerChemistryTasksWorker.UpdateTaskAsync(model);
        }

        /// <summary>
        /// Создать задание
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Admin/Create")]
        public Task<BaseApiResponse> AdminCreate(ChemistryCreateTask model)
        {
            return AdminChemistryTasksWorker.CreateTaskAsync(model);
        }

        [HttpPost("Admin/Edit")]
        public Task<BaseApiResponse> AdminEdit(EditChemistryTask model)
        {
            return AdminChemistryTasksWorker.EditTaskAsync(model);
        }

        [HttpPost("Admin/Remove")]
        public Task<BaseApiResponse> AdminRemove(string id)
        {
            return AdminChemistryTasksWorker.RemoveTaskAsync(id);
        }


        [HttpPost("Admin/CancelRemove")]
        public Task<BaseApiResponse> AdminCancelRemove(string id)
        {
            return AdminChemistryTasksWorker.CancelRemoveTaskAsync(id);
        }

        public TaskApiController(ChemistryDbContext context, ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(context, userManager, signInManager)
        {
        }
    }
}
