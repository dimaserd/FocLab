using System.ComponentModel.DataAnnotations.Schema;
using Croco.Core.Model.Abstractions.Auditable;
using Croco.Core.Model.Abstractions.Entity;
using Croco.Core.Model.Models;
using FocLab.Model.Entities.Users.Default;
using Newtonsoft.Json;

namespace FocLab.Model.Entities.Tasker
{
    public class ApplicationDayTaskComment : DayTaskComment<ApplicationDayTask, ApplicationUser>
    {

    }

    /// <summary>
    /// Комментарий к заданию от пользователя
    /// </summary>
    /// <typeparam name="TDayTask"></typeparam>
    /// <typeparam name="TUser"></typeparam>
    public class DayTaskComment<TDayTask, TUser> : AuditableEntityBase, IAuditableStringId where TDayTask : class where TUser : class, ICrocoUser
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
        [ForeignKey(nameof(Author))]
        public string AuthorId { get; set; }

        /// <summary>
        /// Автор данного комменатрия к заданию
        /// </summary>
        [JsonIgnore]
        public virtual TUser Author { get; set; }
    }
}
