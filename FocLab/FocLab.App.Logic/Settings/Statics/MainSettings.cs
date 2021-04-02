using Croco.Core.Application;
using FocLab.App.Logic.Settings.Models;

namespace FocLab.App.Logic.Settings.Statics
{
    public static class MainSettings
    {
        private static SettingsModel Model => CrocoApp.Application.SettingsFactory.GetSetting<SettingsModel>();

        public static string ApplicationName => Model.ApplicationName;
    }
}