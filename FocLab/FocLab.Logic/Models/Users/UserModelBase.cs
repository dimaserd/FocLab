namespace FocLab.Logic.Models
{
    public class UserModelBase
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string Email { get; set; }
    }
}