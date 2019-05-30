using FocLab.Model.Entities;
using FocLab.Model.Enumerations;

namespace FocLab.Logic.EntityDtos
{
    /// <summary>
    /// Файл для химического задания
    /// </summary>
    public class ChemistryTaskDbFileDto
    {
        /// <summary>
        /// Идентификатор задания
        /// </summary>
        public string ChemistryTaskId { get; set; }

        /// <summary>
        /// Задание
        /// </summary>
        public ChemistryTaskDto ChemistryTask { get; set; }
        
        /// <summary>
        /// Идентификатор файла
        /// </summary>
        public string FileId { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        public DbFileDto File { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public ChemistryTaskDbFileType Type { get; set; }


    }
}