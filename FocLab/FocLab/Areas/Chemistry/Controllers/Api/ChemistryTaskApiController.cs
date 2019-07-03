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
    public class ChemistryTaskApiController : CustomChemistryBaseApiController
    {
        private AdminChemistryTasksWorker AdminChemistryTasksWorker => new AdminChemistryTasksWorker(ContextWrapper);

        /// <summary>
        /// Создать задание
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("Create")]
        public Task<BaseApiResponse> Create(ChemistryCreateTask model)
        {
            return AdminChemistryTasksWorker.CreateTaskAsync(model);
        }

        [HttpPost, Route("Edit")]
        public Task<BaseApiResponse> Edit(EditChemistryTask model)
        {
            return AdminChemistryTasksWorker.EditTaskAsync(model);
        }

        [HttpPost, Route("Remove")]
        public Task<BaseApiResponse> Remove(string id)
        {
            return AdminChemistryTasksWorker.RemoveTaskAsync(id);
        }


        [HttpPost, Route("CancelRemove")]
        public Task<BaseApiResponse> CancelRemove(string id)
        {
            return AdminChemistryTasksWorker.CancelRemoveTaskAsync(id);
        }

        public ChemistryTaskApiController(ChemistryDbContext context, ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(context, userManager, signInManager)
        {
        }
    }
}
