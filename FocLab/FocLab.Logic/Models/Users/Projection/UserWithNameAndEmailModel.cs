using System.ComponentModel.DataAnnotations;

namespace FocLab.Logic.Models.Users.Projection
{
    public class UserWithNameAndEmailAvatarModel : UserIdModel
    {
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Идентификатор аватара")]
        public int? AvatarFileId { get; set; }
    }

    public class UserFullNameEmailAndAvatarModel : UserWithNameAndEmailAvatarModel
    {
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }
    }
}
