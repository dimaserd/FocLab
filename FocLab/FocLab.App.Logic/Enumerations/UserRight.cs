using System.ComponentModel.DataAnnotations;

namespace FocLab.App.Logic.Enumerations
{
    /// <summary>
    /// Перечисление описывающее право пользователя
    /// </summary>
    public enum UserRight
    {
        /// <summary>
        /// Администратор
        /// </summary>
        [Display(Name = "Админ")]
        Admin
    }
}