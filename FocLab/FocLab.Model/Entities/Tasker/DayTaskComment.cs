﻿using System.ComponentModel.DataAnnotations.Schema;
using Croco.Core.Model.Models;
using Newtonsoft.Json;

namespace FocLab.Model.Entities.Tasker
{
    public class ApplicationDayTaskComment : DayTaskComment<ApplicationDayTask>
    {

    }

    /// <summary>
    /// Комментарий к заданию от пользователя
    /// </summary>
    /// <typeparam name="TDayTask"></typeparam>
    /// <typeparam name="TUser"></typeparam>
    public class DayTaskComment<TDayTask> : AuditableEntityBase where TDayTask : class
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Текст комментария
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Идентификатор задания к которому был оставлен комментарий
        /// </summary>
        [ForeignKey(nameof(DayTask))]
        public string DayTaskId { get; set; }

        /// <summary>
        /// Задание к которому был оставлен комментарий
        /// </summary>
        [JsonIgnore]
        public virtual TDayTask DayTask { get; set; }

        /// <summary>
        /// Идентификатор автора данного комменатрия к заданию
        /// </summary>
        public string AuthorId { get; set; }
    }
}