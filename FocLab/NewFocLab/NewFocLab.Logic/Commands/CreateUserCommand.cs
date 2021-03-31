using NewFocLab.Logic.Models.Users;

namespace NewFocLab.Logic.Commands
{
    public class CreateUserCommand
    {
        /// <summary>
        /// Пользователь
        /// </summary>
        public UserModelBase User { get; set; }
    }
}