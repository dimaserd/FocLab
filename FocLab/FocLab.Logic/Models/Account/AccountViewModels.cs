using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FocLab.Logic.Models
{

    public class LoginModelBase
    {
        /// <summary>
        /// Пароль
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать пароль")]
        [DataType(DataType.Password)]
        [Description("Пароль")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        /// <summary>
        /// Запомнить пользователя (Если свойство указано как true, то пользователь будет залогинен навечно)
        /// </summary>
        [Description("Запомнить меня")]
        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }

    public class LoginByPhoneNumberModel : LoginModelBase
    {
        [Required(ErrorMessage = "Необходимо указать номер телефона")]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

    }

    public class LoginModel : LoginModelBase
    {
        public LoginModel()
        {

        }

        public LoginModel(LoginModelBase model, string email)
        {
            Email = email;

            Password = model.Password;
            RememberMe = model.RememberMe;
        }

        [Required(ErrorMessage = "Необходимо указать адрес электронной почты")]
        [Display(Name = "Адрес электронной почты")]
        [EmailAddress]
        public string Email { get; set; }

    }



    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        //[Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordByAdminModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }

    public class ResetPasswordByUserModel : ResetPasswordByAdminModel
    {

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        //[Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Почта")]
        public string Email { get; set; }

        public bool SendSms { get; set; }
    }

    public class ForgotPasswordModelByPhone
    {
        [Required(ErrorMessage = "Необходимо указать номер телефона")]
        public string PhoneNumber { get; set; }

        public bool SendEmail { get; set; }
    }
}
