using FocLab.Logic.Models.Users.Projection;

namespace Tms.Logic.Models.Tasker
{
    public class DayTaskCommentModel
    {
        public string Id { get; set; }

        public string Comment { get; set; }

        public UserFullNameEmailAndAvatarModel Author { get; set; }
    }
}