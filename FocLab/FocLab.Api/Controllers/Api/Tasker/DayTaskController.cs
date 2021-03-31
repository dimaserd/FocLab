using System.Threading.Tasks;
using Croco.Core.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using Tms.Logic.Models;
using Tms.Logic.Services;

namespace FocLab.Api.Controllers.Api.Tasker
{
    /// <inheritdoc />
    /// <summary>
    /// Предоставляет методы для создания и редактирования заданий
    /// </summary>
    [Route("Api/DayTask")]
    public class DayTaskController : Controller
    {
        private DayTasksService DayTasksService { get; }
        private DayTaskCommentService TaskCommentService { get; }

        /// <inheritdoc />
        public DayTaskController(DayTasksService dayTasksService, DayTaskCommentService taskCommentService)
        {
            DayTasksService = dayTasksService;
            TaskCommentService = taskCommentService;
        }

        
        /// <summary>
        /// Добавить коментарий к заданию
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Comments/Add")]
        [ProducesDefaultResponseType(typeof(BaseApiResponse<DayTaskModel>))]
        public Task<BaseApiResponse<DayTaskModel>> CommentDayTask([FromBody] CommentDayTask model)
        {
            return TaskCommentService.CommentDayTaskAsync(model);
        }

        /// <summary>
        /// Обновить коментарий к заданию
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Comments/Update")]
        [ProducesDefaultResponseType(typeof(BaseApiResponse<DayTaskModel>))]
        public Task<BaseApiResponse<DayTaskModel>> UpdateDayTaskComment([FromBody] UpdateDayTaskComment model)
        {
            return TaskCommentService.UpdateDayTaskCommentAsync(model);
        }

        /// <summary>
        /// Получение заданий на день
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(DayTaskModel[]))]
        [HttpPost(nameof(GetTasks))]
        public Task<DayTaskModel[]> GetTasks([FromBody] UserScheduleSearchModel model)
        {
            return DayTasksService.GetDayTasksAsync(model);
        }

        /// <summary>
        /// Создание задания
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        [HttpPost("CreateOrUpdate")]
        public Task<BaseApiResponse> CreateOrUpdate([FromBody]CreateOrUpdateDayTask model)
        {
            return DayTasksService.CreateOrUpdateDayTaskAsync(model);
        }
    }
}