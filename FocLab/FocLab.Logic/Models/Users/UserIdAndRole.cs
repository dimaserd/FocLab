using FocLab.Model.Enumerations;

namespace FocLab.Logic.Models.Users
{
    public class UserIdAndRole
    {
        public string UserId { get; set; }
        public UserRight Role { get; set; }
    }
}
