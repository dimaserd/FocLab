using System;
using System.Collections.Generic;

namespace FocLab.Logic.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ChemistryTaskObjectJson
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Дата выполнения
        /// </summary>
        public DateTime? PerformedDate { get; set; }

        /// <summary>
        /// Крайняя дата выполнения
        /// </summary>
        public DateTime DeadLineDate { get; set; }

        /// <summary>
        /// Метод выполнения (ссылка на файл)
        /// </summary>
        public int? MethodFileId { get; set; }

        /// <summary>
        /// Исполнитель
        /// </summary>
        public Chemistry_Task_Performer Performer { get; set; }

        /// <summary>
        /// Метод файл
        /// </summary>
        public Chemistry_TaskMethodFile MethodFile { get; set; }

        /// <summary>
        /// Счетчик веществ
        /// </summary>
        public Chemistry_SubstanceCounter SubstanceCounter { get; set; }

        /// <summary>
        /// Колличество установленное админом
        /// </summary>
        public string AdminQuantity { get; set; }

        /// <summary>
        /// Качество установленное администратором
        /// </summary>
        public string AdminQuality { get; set; }

        /// <summary>
        /// Колличество установленное исполнителем
        /// </summary>
        public string PerformerQuantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PerformerQuality { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PerformerText { get; set; }

        /// <summary>
        /// Эксперименты
        /// </summary>
        public List<Chemistry_Task_Experiment> Experiments { get; set; }

        #region Файлы

        /// <summary>
        /// Изображение реакции
        /// </summary>
        public ChemistryTaskPerformerFile ReactionSchemaImage { get; set; }

        /// <summary>
        /// Файл 1
        /// </summary>
        public ChemistryTaskPerformerFile File1 { get; set; }

        /// <summary>
        /// Файл 2
        /// </summary>
        public ChemistryTaskPerformerFile File2 { get; set; }

        /// <summary>
        /// Файл 3
        /// </summary>
        public ChemistryTaskPerformerFile File3 { get; set; }

        /// <summary>
        /// Файл 4
        /// </summary>
        public ChemistryTaskPerformerFile File4 { get; set; }

        #endregion
    }
}