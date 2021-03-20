using System.Threading.Tasks;
using FocLab.Logic.Abstractions;
using FocLab.Model.Entities.Users.Default;
using Microsoft.AspNetCore.Identity;

namespace FocLab.Logic.Implementations
{
    public class ClientDataRefresher : IClientDataRefresher
    {
        SignInManager<ApplicationUser> SignInManager { get; }

        public ClientDataRefresher(SignInManager<ApplicationUser> signInManager)
        {
            SignInManager = signInManager;
        }
        
        public Task RefreshUserData(ApplicationUser applicationUser)
        {
            return SignInManager.SignInAsync(applicationUser, true);
        }
    }
}