using System.ComponentModel.DataAnnotations;

namespace FocLab.Logic.Models.Account
{
    public class ForgotPasswordModelByPhone
    {
        [Required(ErrorMessage = "Необходимо указать номер телефона")]
        public string PhoneNumber { get; set; }

        public bool SendEmail { get; set; }
    }
}
