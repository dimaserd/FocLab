using Croco.Core.Abstractions.Settings;
using FocLab.Logic.Settings.Statics;

namespace FocLab.Logic.Settings.Models
{
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
}
