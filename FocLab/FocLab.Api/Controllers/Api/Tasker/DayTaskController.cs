using System.Collections.Generic;
using System.Threading.Tasks;
using Croco.Core.Contract;
using Croco.Core.Contract.Models;
using FocLab.Api.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Tms.Logic.Models.Tasker;
using Tms.Logic.Workers.Tasker;

namespace FocLab.Api.Controllers.Api.Tasker
{
    /// <inheritdoc />
    /// <summary>
    /// Предоставляет методы для создания и редактирования заданий
    /// </summary>
    [Route("Api/DayTask")]
    public class DayTaskController : BaseApiController
    {
        private DayTasksWorker DayTaskWorker { get; }

        /// <inheritdoc />
        public DayTaskController(DayTasksWorker dayTasksWorker,
            ICrocoRequestContextAccessor requestContextAccessor) : base(requestContextAccessor)
        {
            DayTaskWorker = dayTasksWorker;
        }

        
        /// <summary>
        /// Добавить коментарий к заданию
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Comments/Add")]
        [ProducesDefaultResponseType(typeof(BaseApiResponse<DayTaskModel>))]
        public Task<BaseApiResponse<DayTaskModel>> CommentDayTask([FromForm]CommentDayTask model)
        {
            return DayTaskWorker.CommentDayTaskAsync(model);
        }

        /// <summary>
        /// Обновить коментарий к заданию
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Comments/Update")]
        [ProducesDefaultResponseType(typeof(BaseApiResponse<DayTaskModel>))]
        public Task<BaseApiResponse<DayTaskModel>> UpdateDayTaskComment([FromForm]UpdateDayTaskComment model)
        {
            return DayTaskWorker.UpdateDayTaskCommentAsync(model);
        }

        /// <summary>
        /// Получение заданий на день
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(List<DayTaskModel>))]
        [HttpPost(nameof(GetTasks))]
        public Task<List<DayTaskModel>> GetTasks([FromForm]UserScheduleSearchModel model)
        {
            return DayTaskWorker.GetDayTasksAsync(model);
        }

        /// <summary>
        /// Создание задания
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        [HttpPost("CreateOrUpdate")]
        public Task<BaseApiResponse> CreateOrUpdate([FromForm]CreateOrUpdateDayTask model)
        {
            return DayTaskWorker.CreateOrUpdateDayTaskAsync(model);
        }
    }
}