using Croco.Core.Application;
using FocLab.Logic.Settings.Models;

namespace FocLab.Logic.Settings.Statics
{
    public static class MainSettings
    {
        private static SettingsModel Model => CrocoApp.Application.SettingsFactory.GetSetting<SettingsModel>();

        public static string ApplicationName => Model.ApplicationName;
    }
}