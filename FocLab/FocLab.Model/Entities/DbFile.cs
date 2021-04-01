using System.Collections.Generic;
using Croco.Core.Model.Entities.Application;

namespace FocLab.Model.Entities
{
    /// <summary>
    /// Класс описывающий файл который находится в базе данных
    /// </summary>
    public class DbFile : DbFileIntId
    {
        public virtual ICollection<ApplicationDbFileHistory> History { get; set; }
    }

    public class ApplicationDbFileHistory : DbFileHistory<DbFile>
    {
    }
}
