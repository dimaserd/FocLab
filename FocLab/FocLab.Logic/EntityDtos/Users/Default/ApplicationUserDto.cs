using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using FocLab.Model.Entities.Users.Default;
using FocLab.Model.Enumerations;

namespace FocLab.Logic.EntityDtos.Users.Default
{
    public class ApplicationUserDto
    {
        [Display(Name = "Идентификатор")]
        public string Id { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public string UnConfirmedEmail { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Email подтвержден")]
        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Номер телефона подтвержден")]
        public bool PhoneNumberConfirmed { get; set; }

        [Display(Name = "Баланс")]
        public decimal Balance { get; set; }

        [Display(Name = "Пол")]
        public bool? Sex { get; set; }

        [Display(Name = "JSON Объект")]
        public string ObjectJson { get; set; }
        

        /// <summary>
        /// Дата рождения
        /// </summary>
        [Display(Name = "Дата рождения")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Деактивирован")]
        public bool DeActivated { get; set; }

        [Display(Name = "Роли пользователя")]
        public List<ApplicationUserRoleDto> Roles { get; set; }

        /// <summary>
        /// Идентификатор файла для аватара
        /// </summary>
        public int? AvatarFileId { get; set; }

        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        public string SecurityStamp { get; set; }

        #region Свойства для аудита
        public string CurrentSnapshotId { get; set; }

        
        public string CreatedBy { get; set; }

        [Display(Name = "Дата регистрации")]
        public DateTime CreatedOn { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public string LastModifiedBy { get; set; }

        #endregion

        public bool HasRight(UserRight userRight)
        {
            return Roles.Any(x => x.RoleName == userRight.ToString());
        }

        public static Expression<Func<ApplicationUser, ApplicationUserDto>> SelectExpression = x => new ApplicationUserDto
        {
            Id = x.Id,
            Email = x.Email,
            Name = x.Name,
            Surname = x.Surname,
            Patronymic = x.Patronymic,
            ObjectJson = x.ObjectJson,
            Balance = x.Balance,
            CreatedOn = x.CreatedOn,

            PasswordHash = x.PasswordHash,
            PhoneNumber = x.PhoneNumber,
            PhoneNumberConfirmed = x.PhoneNumberConfirmed,
            DeActivated = x.DeActivated,
            EmailConfirmed = x.EmailConfirmed,
            BirthDate = x.BirthDate,
            Sex = x.Sex,
            UserName = x.UserName,
            AvatarFileId = x.AvatarFileId,

            Roles = x.Roles.Select(t => new ApplicationUserRoleDto
            {
                RoleId = t.RoleId,
                RoleName = t.Role.Name,
                UserId = t.UserId
            }).ToList()
        };
    }
}