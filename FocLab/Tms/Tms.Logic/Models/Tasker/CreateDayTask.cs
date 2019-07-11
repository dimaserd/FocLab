using System;

namespace Tms.Logic.Models.Tasker
{
    public class CreateDayTask
    {
        public DateTime TaskDate { get; set; }

        /// <summary>
        /// Описание задания
        /// </summary>
        public string TaskText { get; set; }

        /// <summary>
        /// Название задания
        /// </summary>
        public string TaskTitle { get; set; }

        public string AssigneeUserId { get; set; }
    }

}
