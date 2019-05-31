using System;
using FocLab.Model.Entities.Chemistry;

namespace FocLab.Logic.Models
{
    /// <summary>
    /// Химический метод файла
    /// </summary>
    public class Chemistry_TaskMethodFile
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="model"></param>
        public Chemistry_TaskMethodFile(ChemistryMethodFile model)
        {
            Name = model.Name;
            FileId = model.FileId;
            CreationDate = model.CreationDate;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Chemistry_TaskMethodFile()
        {

        }


        /// <summary>
        /// Название 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор файла
        /// </summary>
        public int FileId { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Получить ссылку на файл
        /// </summary>
        /// <returns></returns>
        public string GetLinkToFile()
        {
            return $"/Files/GetDbFileById/{FileId}";
        }
    }
}