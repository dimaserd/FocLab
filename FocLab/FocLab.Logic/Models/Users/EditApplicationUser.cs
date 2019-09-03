﻿using System;
using System.ComponentModel.DataAnnotations;
using FocLab.Logic.Resources;

namespace FocLab.Logic.Models.Users
{
    public class EditApplicationUser
    {
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.UserIdIsRequired))]
        public string Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.EmailIsRequired))]
        [EmailAddress(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.EmailIsNotValid))]
        public string Email { get; set; }

        [Display(Name = "Дата рождения")]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        [Display(Name = "Пол")]
        public bool? Sex { get; set; } 

        public string ObjectJson { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
    }
}
