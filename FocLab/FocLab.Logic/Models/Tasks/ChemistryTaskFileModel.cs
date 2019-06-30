﻿using FocLab.Model.Enumerations;

namespace FocLab.Logic.Models
{
    public class ChemistryTaskFileModel
    {
        /// <summary>
        /// Идентификатор файла
        /// </summary>
        public int FileId { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public ChemistryTaskDbFileType Type { get; set; }
    }
}