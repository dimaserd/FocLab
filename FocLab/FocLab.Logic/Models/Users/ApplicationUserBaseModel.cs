using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using FocLab.Logic.Models.Users.Projection;
using FocLab.Model.Entities.Users.Default;
using FocLab.Model.Enumerations;
using Newtonsoft.Json;

namespace FocLab.Logic.Models.Users
{
    public class ApplicationUserBaseModel : UserWithNameAndEmailAvatarModel
    {
        public string UnConfirmedEmail { get; set; }
        
        [Display(Name = "Email подтвержден")]
        public bool EmailConfirmed { get; set; }
        
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
        
        [Display(Name = "Права пользователя")]
        public List<UserRight> Rights { get; set; }

        
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [JsonIgnore]
        internal static Expression<Func<ApplicationUser, ApplicationUserBaseModel>> SelectExpression = x => new ApplicationUserBaseModel
        {
            Id = x.Id,
            Name = x.Name,
            Surname = x.Surname,
            Email = x.Email,
            ObjectJson = x.ObjectJson,
            PhoneNumber = x.PhoneNumber,
            DeActivated = x.DeActivated,
            EmailConfirmed = x.EmailConfirmed,
            PhoneNumberConfirmed = x.PhoneNumberConfirmed,
            Balance = x.Balance,
            BirthDate = x.BirthDate,
            Patronymic = x.Patronymic,
            Sex = x.Sex,
            UserName = x.UserName,
            UnConfirmedEmail = x.UnConfirmedEmail,
            AvatarFileId = x.AvatarFileId
        };
    }
}
