using Croco.Core.Settings;
using FocLab.Logic.Settings.Models;

namespace FocLab.Logic.Settings.Statics
{
    public static class MainSettings
    {
        private static SettingsModel Model => new CommonSettingsFactory().GetSetting<SettingsModel>();

        public static string ApplicationName => Model.ApplicationName;

    }
}
