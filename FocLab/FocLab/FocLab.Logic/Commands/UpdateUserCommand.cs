using FocLab.Logic.Models.Users;

namespace FocLab.Logic.Commands
{
    public class UpdateUserCommand
    {
        /// <summary>
        /// Пользователь
        /// </summary>
        public UserModelBase User { get; set; }
    }
}
