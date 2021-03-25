using System.ComponentModel.DataAnnotations;

namespace FocLab.Logic.Models.Account
{
    public class LoginByPhoneNumberModel : LoginModelBase
    {
        [Required(ErrorMessage = "Необходимо указать номер телефона")]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

    }
}