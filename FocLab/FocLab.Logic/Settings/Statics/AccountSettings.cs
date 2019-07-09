using Croco.Core.Application;
using Croco.Core.Settings;
using FocLab.Logic.Settings.Models;

namespace FocLab.Logic.Settings.Statics
{
    public enum ConfirmLoginType
    {
        None,

        BySms,

        ByEmail
    }

    public class AccountSettings
    {
        private static AccountSettingsModel Model => CrocoApp.Application.SettingsFactory.GetSetting<AccountSettingsModel>();


        /// <summary>
        /// Разрешено логинирование для пользователей которые не подтвердили Email
        /// </summary>
        public static bool IsLoginEnabledForUsersWhoDidNotConfirmEmail => Model.IsLoginEnabledForUsersWhoDidNotConfirmEmail;

        public static bool ShouldUsersConfirmEmail => Model.ShouldUsersConfirmEmail;

        public static bool RegistrationEnabled => Model.RegistrationEnabled;

        public static ConfirmLoginType ConfirmLogin => Model.ConfirmLogin;
    }
}
