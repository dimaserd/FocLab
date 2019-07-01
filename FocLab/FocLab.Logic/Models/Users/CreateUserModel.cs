using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FocLab.Logic.Models.Account;
using FocLab.Model.Enumerations;

namespace FocLab.Logic.Models.Users
{
    public class CreateUserModel : RegisterModel
    {
        [Display(Name = "Права пользователя")]
        public List<UserRight> Rights { get; set; }
    }
}
