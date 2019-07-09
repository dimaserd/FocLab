using Croco.Core.Abstractions;
using Croco.Core.Abstractions.Cache;
using Croco.Core.Abstractions.Settings;
using Croco.Core.Application;
using Croco.Core.Cache;
using Croco.Core.Settings;
using Croco.Core.Utils;
using System;
using System.IO;

namespace FocLab.Logic.Implementations
{
    /// <summary>
    /// Фабрика общих настроек для приложения
    /// </summary>
    public class TempCommonSettingsFactory : ISettingsFactory
    {
        private const string Prefix = "Setting_";
        private readonly TimeSpan _cacheTimeSpan = TimeSpan.FromDays(20);
        private readonly ICrocoCacheManager _cacheProvider;

        /// <summary>
        /// Конструктор
        /// </summary>
        public TempCommonSettingsFactory(ICrocoCacheManager cacheManager)
        {
            _cacheProvider = cacheManager;
        }

        private static string GetKey<T>() where T : class, ICommonSetting<T>
        {
            return $"{Prefix}{typeof(T).Name}";
        }

        /// <summary>
        /// Обновить настройку
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="setting"></param>
        public void UpdateSetting<T>(T setting) where T : class, ICommonSetting<T>
        {
            if (setting == null)
            {
                throw new NullReferenceException(nameof(setting));
            }

            WriteSettingsToFile(setting);
            Remove<T>();
        }

        /// <summary>
        /// Получить настройку
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetSetting<T>() where T : class, ICommonSetting<T>, new()
        {
            var key = GetKey<T>();

            if (_cacheProvider.GetValue<T>(key) is T result)
            {
                return result;
            }

            //Блокируем остальные входные потоки
            lock (Locker)
            {
                result = _cacheProvider.GetValue<T>(key);

                if (result != null)
                {
                    return result;
                }

                result = ReadSettingsFromFile<T>();

                _cacheProvider.AddValue(new CrocoCacheValue
                {
                    Key = key,
                    Value = result,
                    AbsoluteExpiration = DateTime.Now.Add(_cacheTimeSpan)
                });
            }

            return result;
        }


        /// <summary>
        /// Удалить настройки
        /// </summary>
        public void ClearSettings()
        {
            //TODO Реализовать очистку настроек
            throw new NotImplementedException();
        }

        #region Вспомогательные методы

        private void Remove<T>() where T : class, ICommonSetting<T>
        {
            var key = GetKey<T>();

            _cacheProvider.Remove(key);
        }

        private static readonly object Locker = new object();

        private void WriteSettingsToFile<T>(T model) where T : class, ICommonSetting<T>
        {
            var fileName = $"{typeof(T).Name}.json";

            var filePath = CrocoApp.Application.MapPath($"{SettingsConfiguration.SettingsDirectory}/{fileName}");

            // ReSharper disable once AssignNullToNotNullAttribute
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            // ReSharper disable once AssignNullToNotNullAttribute
            File.WriteAllText(filePath, Tool.JsonConverter.Serialize(model));

            Remove<T>();
        }

        private T ReadSettingsFromFile<T>() where T : class, ICommonSetting<T>, new()
        {
            var fileName = $"{typeof(T).Name}.json";

            var filePath = CrocoApp.Application.MapPath($"{SettingsConfiguration.SettingsDirectory}/{fileName}");

            if (!File.Exists(filePath))
            {
                var model = new T();

                var result = model.GetDefault();

                WriteSettingsToFile(result);

                return result;
            }

            var fileContents = FileTool.ReadAllTextNoLock(filePath);

            return Tool.JsonConverter.Deserialize<T>(fileContents);
        }

        #endregion
    }
}
