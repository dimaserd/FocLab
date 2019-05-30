﻿using System;
using System.ComponentModel.DataAnnotations;
using FocLab.Logic.Resources;

namespace FocLab.Logic.Models.Users
{
    public class EditApplicationUser : UserIdModel
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.EmailIsRequired))]
        [EmailAddress(ErrorMessageResourceType = typeof(ValidationMessages), ErrorMessageResourceName = nameof(ValidationMessages.EmailIsNotValid))]
        public string Email { get; set; }

        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string Surname { get; set; } 

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; } 

        /// <summary>
        /// Пол
        /// </summary>
        public bool? Sex { get; set; } 

        public string ObjectJson { get; set; } 

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string PhoneNumber { get; set; }
        
    }
}
