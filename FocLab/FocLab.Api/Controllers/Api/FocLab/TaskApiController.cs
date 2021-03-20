using System.Collections.Generic;
using System.Threading.Tasks;
using Croco.Core.Contract.Models;
using FocLab.Logic.Models;
using FocLab.Logic.Models.Tasks;
using FocLab.Logic.Workers.ChemistryTasks;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Api.Controllers.Api.FocLab
{
    /// <inheritdoc />
    /// <summary>
    /// Апи контроллер предоставляющий методы для работы с химическими заданиями
    /// </summary>
    [Route("Api/Chemistry/Tasks")]
    public class TaskApiController : Controller
    {
        private AdminChemistryTasksWorker AdminChemistryTasksWorker { get; }

        private PerformerChemistryTasksWorker PerformerChemistryTasksWorker { get; }

        private ChemistryTasksWorker ChemistryTasksWorker { get; }

        public TaskApiController(
            AdminChemistryTasksWorker adminChemistryTasksWorker,
            PerformerChemistryTasksWorker performerChemistryTasksWorker,
            ChemistryTasksWorker chemistryTasksWorker)
        {
            AdminChemistryTasksWorker = adminChemistryTasksWorker;
            PerformerChemistryTasksWorker = performerChemistryTasksWorker;
            ChemistryTasksWorker = chemistryTasksWorker;
        }

        [HttpGet("GetAll")]
        public Task<List<ChemistryTaskModel>> GetTasks()
        {
            return ChemistryTasksWorker.GetAllTasksAsync();
        }

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
        /// Изменить тип файла для задания
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("ChangeFileForTask")]
        public Task<BaseApiResponse> ChangeFileForTask(ChemistryChangeFileForTask model)
        {
            return ChemistryTasksWorker.ChangeFileFileForTaskAsync(model);
        }

        /// <summary>
        /// Пометить задание, как завершенное
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Performer/Perform")]
        public Task<BaseApiResponse> PerformerPerformTask(PerformTaskModel model)
        {
            return PerformerChemistryTasksWorker.PerformTaskAsync(model);
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
    }
}
