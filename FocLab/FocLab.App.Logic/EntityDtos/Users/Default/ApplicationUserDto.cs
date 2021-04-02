using System;
using System.ComponentModel.DataAnnotations;

namespace FocLab.App.Logic.EntityDtos.Users.Default
{
    public class ApplicationUserDto
    {
        [Display(Name = "Идентификатор")]
        public string Id { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

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

        [Display(Name = "Дата регистрации")]
        public DateTime CreatedOn { get; set; }
    }
}