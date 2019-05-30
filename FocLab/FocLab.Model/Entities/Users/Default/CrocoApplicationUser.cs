using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Croco.Core.Model.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace FocLab.Model.Entities.Users.Default
{
    public class WebApplicationUser : IdentityUser, ICrocoUser
    {

        /// <inheritdoc />
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; }


        #region Свойства для аудита
        [ConcurrencyCheck]
        public string CurrentSnapshotId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }

        #endregion
    }

    public class WebApplicationUser<TAvatarFile> : WebApplicationUser, ICrocoUser<TAvatarFile> where TAvatarFile : class
    {
        /// <inheritdoc />
        /// <summary>
        /// Идентификатор файла с аватаром пользователя
        /// </summary>
        [ForeignKey(nameof(AvatarFile))]
        public int? AvatarFileId { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Аватар пользователя
        /// </summary>
        [JsonIgnore]
        public virtual TAvatarFile AvatarFile { get; set; }
    }
}
