using FocLab.Logic.Services;
using FocLab.Model.Entities.Users.Default;
using FocLab.Model.Enumerations;
using Microsoft.AspNetCore.Identity;

namespace FocLab.Logic.Workers.Account
{
    public static class UserManagerExtensions
    {
        /// <summary>
        /// Добавляет право пользователю
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="user"></param>
        /// <param name="userRight"></param>
        /// <returns></returns>
        public static IdentityResult AddRight(this ApplicationUserManager userManager, ApplicationUser user, UserRight userRight)
        {   
            return userManager.AddToRoleAsync(user, userRight.ToString()).GetAwaiter().GetResult();
        }
    }
}