using System.ComponentModel.DataAnnotations;
using FocLab.Logic.Resources;

namespace Tms.Logic.Models.Tasker
{
    public class CommentDayTask
    {
        public string DayTaskId { get; set; }

        [Required(ErrorMessageResourceType = typeof(TaskerResource), ErrorMessageResourceName = nameof(TaskerResource.CommentCantBeEmpty))]
        public string Comment { get; set; }
    }
}