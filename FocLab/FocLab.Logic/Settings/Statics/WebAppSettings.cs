using Croco.Core.Application;
using Croco.Core.Settings;
using FocLab.Logic.Settings.Models;

namespace FocLab.Logic.Settings.Statics
{
    public static class WebAppSettings
    {
        private static WebAppSettingsModel Model => CrocoApp.Application.SettingsFactory.GetSetting<WebAppSettingsModel>();

        public static bool UseMainRedirect => Model.UseMainRedirect;

        /// <summary>
        /// Текущий выбранный шаблон
        /// </summary>
        public static string TemplateName => Model.TemplateName;

        public static string RedirectUri => Model.RedirectUri;
    }
}
