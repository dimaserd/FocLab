using Croco.Core.Abstractions.Settings;

namespace FocLab.Logic.Settings.Models
{
    public class SettingsModel : ICommonSetting<SettingsModel>
    {
        public string ApplicationName { get; set; }

        public SettingsModel GetDefault()
        {
            return new SettingsModel
            {
                ApplicationName = "Название приложения не установлено"
            };
        }
    }
}
