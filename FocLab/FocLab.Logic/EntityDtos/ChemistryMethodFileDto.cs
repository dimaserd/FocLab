using FocLab.Model.Entities;
using System;

namespace FocLab.Logic.EntityDtos
{
    /// <summary>
    /// Метод решения (Является файлом)
    /// </summary>
    public class ChemistryMethodFileDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Идентификатор файла
        /// </summary>
        public string FileId { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        public virtual DbFileDto File { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }
    }
}