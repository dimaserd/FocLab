using Croco.Core.Abstractions.Settings;
using Croco.Core.Settings;

namespace FocLab.Logic.Settings.Statics
{
    public enum ConfirmLoginType
    {
        None,

        BySms,

        ByEmail
    }

    public class AccountSettingsModel : ICommonSetting<AccountSettingsModel>
    {
        public bool IsLoginEnabledForUsersWhoDidNotConfirmEmail { get; set; }

        public bool ShouldUsersConfirmEmail { get; set; }

        public bool RegistrationEnabled { get; set; }

        public ConfirmLoginType ConfirmLogin { get; set; }

        public AccountSettingsModel GetDefault()
        {
            return new AccountSettingsModel
            {
                IsLoginEnabledForUsersWhoDidNotConfirmEmail = true,
                ShouldUsersConfirmEmail = false,
                RegistrationEnabled = true,
                ConfirmLogin = ConfirmLoginType.None
            };
        }

    }

    public class AccountSettings
    {
        private static AccountSettingsModel Model => new CommonSettingsFactory().GetSetting<AccountSettingsModel>();


        /// <summary>
        /// Разрешено логинирование для пользователей которые не подтвердили Email
        /// </summary>
        public static bool IsLoginEnabledForUsersWhoDidNotConfirmEmail => Model.IsLoginEnabledForUsersWhoDidNotConfirmEmail;

        public static bool ShouldUsersConfirmEmail => Model.ShouldUsersConfirmEmail;

        public static bool RegistrationEnabled => Model.RegistrationEnabled;

        public static ConfirmLoginType ConfirmLogin => Model.ConfirmLogin;
    }
}
