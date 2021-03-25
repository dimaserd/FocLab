using System.ComponentModel.DataAnnotations;

namespace FocLab.Logic.Models.Account
{
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
}