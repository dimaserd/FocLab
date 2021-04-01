using System;
using System.Linq.Expressions;
using FocLab.Model.Entities;

namespace FocLab.Logic.EntityDtos
{
    public class DbFileDto
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public DateTime CreatedOn { get; set; }

        public static Expression<Func<DbFile, DbFileDto>> SelectExpression = x => new DbFileDto
        {
            Id = x.Id,
            FileName = x.FileName,
            CreatedOn = x.CreatedOn
        };
    }
}