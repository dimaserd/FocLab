using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Croco.Core.Model.Entities.Application;

namespace FocLab.Model.Entities
{
    /// <summary>
    /// Класс описывающий файл который находится в базе данных
    /// </summary>
    public class DbFile : DbFileIntId
    {
        public virtual ICollection<ApplicationDbFileHistory> History { get; set; }

        #region Свойства отношений
        //[JsonIgnore]
        //public virtual ICollection<ProductColorGroupFile> ColorGroupFiles { get; set; }

        //[JsonIgnore]
        //public virtual ICollection<ProductFile> ProductFiles { get; set; }

        #endregion

    }

    public class ApplicationDbFileHistory : DbFileHistory<DbFile>
    {
    }

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
            CreatedOn = x.CreatedOn,
            //ProductFiles = x.ProductFiles.Select(t => new ProductFileDto {ProductId = t.ProductId, Type = t.Type})
            //    .ToList(),
            //ColorGroupFiles = x.ColorGroupFiles.Select(t => new ProductColorGroupFileDto
            //    {
            //        ProductColorGroupId = t.ProductColorGroupId,
            //        Type = t.Type,
            //        ProductColorGroup = new ProductColorGroupDto
            //            {Id = t.ProductColorGroup.Id, ParentProductId = t.ProductColorGroup.ParentProductId}
            //    })
            //    .ToList(),

        };
    }

    

    #region Расширения
    
    #endregion
}
