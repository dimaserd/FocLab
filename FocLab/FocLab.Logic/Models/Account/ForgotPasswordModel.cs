using System.ComponentModel.DataAnnotations;

namespace FocLab.Logic.Models.Account
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Почта")]
        public string Email { get; set; }

        public bool SendSms { get; set; }
    }
}
