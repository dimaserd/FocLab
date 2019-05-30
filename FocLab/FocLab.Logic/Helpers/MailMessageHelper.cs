using Croco.Core.Application;
using FocLab.Logic.EntityDtos.Users.Default;
using FocLab.Logic.Implementations;
using FocLab.Model.Entities.Chemistry;


namespace FocLab.Logic.Helpers
{
    /// <summary>
    /// Вспомогательный класс для создания сообщений
    /// </summary>
    public class MailMessageHelper
    {
        /// <summary>
        /// Получить сообщение
        /// </summary>
        /// <param name="dayTask"></param>
        /// <param name="user"></param>
        /// <param name="adminUser"></param>
        /// <returns></returns>
        public static object GetMailMessage(ChemistryDayTask dayTask, ApplicationUserDto user, ApplicationUserDto adminUser)
        {
            var domainName = ((FocLabWebApplication) CrocoApp.Application).DomainName;

            var link = $"{domainName}/Schedule/Index?DayTaskId={dayTask.Id}&UserId={user.Id}";

            return new
            {
                Body = $"Исполнитель {user.Email} отправил вам уведомление  о проверке <a href='{link}'>задания</a>.",
                Subject = "Проверьте задание",
                UserId = adminUser.Id,
            };
        }
    }
}