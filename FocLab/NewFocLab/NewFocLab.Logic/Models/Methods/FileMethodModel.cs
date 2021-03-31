using System;
using System.Linq.Expressions;
using NewFocLab.Model.Entities;
using Newtonsoft.Json;

namespace FocLab.Logic.Models.Methods
{
    public class FileMethodModel
    {
        public string Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор файла
        /// </summary>
        public int FileId { get; set; }

        [JsonIgnore]
        internal static Expression<Func<ChemistryMethodFile, FileMethodModel>> SelectExpression = x => new FileMethodModel
        {
            Id = x.Id,
            FileId = x.FileId,
            Name = x.Name
        };
    }
}