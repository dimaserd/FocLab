using System.Threading.Tasks;
using FocLab.Logic.Abstractions;
using FocLab.Model.Entities.Users.Default;
using Microsoft.AspNetCore.Identity;

namespace FocLab.Logic.Implementations
{
    public class ApplicationAuthenticationManager : IApplicationAuthenticationManager
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ApplicationAuthenticationManager(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public Task SignOutAsync()
        {
            return _signInManager.SignOutAsync();
        }
    }
}