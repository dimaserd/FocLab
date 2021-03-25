using System.ComponentModel.DataAnnotations;
using FocLab.Logic.Resources;

namespace FocLab.Logic.Models.Users
{
    public class UserIdModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.UserIdIsRequired))]
        public string Id { get; set; }
    }
}