using FocLab.Logic.Models.Users;

namespace FocLab.Logic.Commands
{
    public class CreateUserCommand
    {
        /// <summary>
        /// Пользователь
        /// </summary>
        public UserModelBase User { get; set; }
    }
}