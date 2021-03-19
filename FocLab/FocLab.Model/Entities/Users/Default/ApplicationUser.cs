using Croco.Core.Contract.Data.Entities.HaveId;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FocLab.Model.Entities.Users.Default
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    [Table(nameof(ApplicationUser))]
    public class ApplicationUser : WebApplicationUser<DbFile>, IHaveStringId
    {
        #region Свойства

        
        public string UnConfirmedEmail { get; set; }
        
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Пол (Null - не указано, 0 - женский, 1 - мужской)
        /// </summary>
        public bool? Sex { get; set; }

        /// <summary>
        /// Баланс пользователя
        /// </summary>
        public decimal Balance { get; set; }

        public bool DeActivated { get; set; }
        
        public string ObjectJson { get; set; }
        
        
        #endregion

        public ICollection<ApplicationUserRole> Roles { get; set; }
    }
}