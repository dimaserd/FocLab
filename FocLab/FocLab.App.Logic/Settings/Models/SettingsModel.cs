namespace FocLab.App.Logic.Settings.Models
{
    public class SettingsModel
    {
        public string ApplicationName { get; set; }

        public SettingsModel()
        {
            ApplicationName = "Название приложения не установлено";
        }
    }
}