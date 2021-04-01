using Croco.Core.Application;
using FocLab.Logic.Settings.Models;

namespace FocLab.Logic.Settings.Statics
{
    public static class WebAppSettings
    {
        private static WebAppSettingsModel Model => CrocoApp.Application.SettingsFactory.GetSetting<WebAppSettingsModel>();

        public static bool UseMainRedirect => Model.UseMainRedirect;


        public static string RedirectUri => Model.RedirectUri;
    }
}