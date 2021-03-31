using System.ComponentModel.DataAnnotations;

namespace NewFocLab.Model.Enumerations
{
    /// <summary>
    /// Тип файла для химического задания
    /// </summary>
    public enum ChemistryTaskDbFileType
    {
        /// <summary>
        /// Изображение реакции
        /// </summary>
        [Display(Name = "Изображение реакции")]
        ReactionSchemaImage,

        /// <summary>
        /// Файл 1
        /// </summary>
        [Display(Name = "Файл 1")]
        File1,

        /// <summary>
        /// Файл 2
        /// </summary>
        [Display(Name = "Файл 2")]
        File2,

        /// <summary>
        /// Файл 3
        /// </summary>
        [Display(Name = "Файл 3")]
        File3,

        /// <summary>
        /// Файл 4
        /// </summary>
        [Display(Name = "Файл 4")]
        File4,
    }

}