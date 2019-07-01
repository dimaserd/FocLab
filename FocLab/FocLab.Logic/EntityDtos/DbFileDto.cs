using System;
using System.Linq.Expressions;
using Croco.Core.Model.Entities.Application;
using FocLab.Model.Entities;

namespace FocLab.Logic.EntityDtos
{
    public class DbFileDto : DbFileIntId
    {
        #region Свойства
        
        #region Свойства отношений

        //public List<ProductColorGroupFileDto> ColorGroupFiles { get; set; }

        //public List<ProductFileDto> ProductFiles { get; set; }

        #endregion

        #endregion

        public static Expression<Func<DbFile, DbFileDto>> SelectExpression = x => new DbFileDto
        {
            Id = x.Id,
            FileName = x.FileName,
            CreatedOn = x.CreatedOn
        };
    }
}