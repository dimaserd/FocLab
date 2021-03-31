using NewFocLab.Model.Entities;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;

namespace FocLab.Logic.Models.Methods
{
    public class ChemistryMethodFileModel
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
        public int FileId { get; set; }


        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }

        public string GetLinkToFile()
        {
            return $"/Files/GetDbFileById/{FileId}";
        }

        [JsonIgnore]
        internal static Expression<Func<ChemistryMethodFile, ChemistryMethodFileModel>> SelectExpression = x => new ChemistryMethodFileModel
        {
            Id = x.Id,
            FileId = x.FileId,
            Name = x.Name,
            CreationDate = x.CreationDate
        };
    }
}
