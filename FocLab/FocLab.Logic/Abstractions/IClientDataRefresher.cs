using System.Threading.Tasks;
using FocLab.Model.Entities.Users.Default;

namespace FocLab.Logic.Abstractions
{
    public interface IClientDataRefresher
    {
        Task RefreshUserData(ApplicationUser applicationUser);
    }
}