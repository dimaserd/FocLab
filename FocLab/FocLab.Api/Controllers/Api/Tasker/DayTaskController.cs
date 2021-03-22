﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Croco.Core.Contract.Models;
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
    public class DayTaskController : Controller
    {
        private DayTasksWorker DayTaskWorker { get; }

        /// <inheritdoc />
        public DayTaskController(DayTasksWorker dayTasksWorker)
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
        public Task<BaseApiResponse<DayTaskModel>> CommentDayTask(CommentDayTask model)
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
        public Task<BaseApiResponse<DayTaskModel>> UpdateDayTaskComment(UpdateDayTaskComment model)
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
        public Task<List<DayTaskModel>> GetTasks(UserScheduleSearchModel model)
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
        public Task<BaseApiResponse> CreateOrUpdate(CreateOrUpdateDayTask model)
        {
            return DayTaskWorker.CreateOrUpdateDayTaskAsync(model);
        }
    }
}