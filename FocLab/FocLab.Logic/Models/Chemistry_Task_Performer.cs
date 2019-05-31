using FocLab.Logic.EntityDtos.Users.Default;

namespace FocLab.Logic.Models
{
    /// <summary>
    /// Исполнитель химического задания
    /// </summary>
    public class Chemistry_Task_Performer
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="user"></param>
        public Chemistry_Task_Performer(ApplicationUserDto user)
        {
            UserId = user.Id;
            Name = user.Name;
            Email = user.Email;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Chemistry_Task_Performer()
        {

        }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string UserId { get; set; }

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