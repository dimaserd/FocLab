using NewFocLab.Logic.Models.Users;

namespace NewFocLab.Logic.Commands
{
    public class UpdateUserCommand
    {
        /// <summary>
        /// Пользователь
        /// </summary>
        public UserModelBase User { get; set; }
    }
}
