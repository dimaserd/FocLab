using Croco.Core.Abstractions.Settings;

namespace FocLab.Logic.Settings.Models
{
    /// <summary>
    /// Класс настроек для веб-приложения
    /// </summary>
    public class WebAppSettingsModel : ICommonSetting<WebAppSettingsModel>
    {
        /// <summary>
        /// Использовать редирект на главной странице 
        /// </summary>
        public bool UseMainRedirect { get; set; }

        /// <summary>
        /// Uri адрес куда производить редирект
        /// </summary>
        public string RedirectUri { get; set; }
        
        /// <summary>
        /// Имя стартового шаблона (Папка во ViewTemplates)
        /// </summary>
        public string TemplateName { get; set; }

        public WebAppSettingsModel GetDefault()
        {
            return new WebAppSettingsModel
            {
                RedirectUri = "",
                UseMainRedirect = false,
                TemplateName = "Standard"
            };
        }
    }
}