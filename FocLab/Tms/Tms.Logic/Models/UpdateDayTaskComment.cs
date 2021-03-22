using FocLab.Logic.Resources;
using System.ComponentModel.DataAnnotations;

namespace Tms.Logic.Models
{
    public class UpdateDayTaskComment
    {
        public string DayTaskCommentId { get; set; }

        [Required(ErrorMessageResourceType = typeof(TaskerResource), ErrorMessageResourceName = nameof(TaskerResource.CommentCantBeEmpty))]
        public string Comment { get; set; }
    }
}