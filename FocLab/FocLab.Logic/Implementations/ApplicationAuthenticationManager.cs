using System.Threading.Tasks;
using FocLab.Logic.Abstractions;
using FocLab.Logic.Services;

namespace FocLab.Logic.Implementations
{
    public class ApplicationAuthenticationManager : IApplicationAuthenticationManager
    {
        private readonly ApplicationSignInManager _signInManager;

        public ApplicationAuthenticationManager(ApplicationSignInManager signInManager)
        {
            _signInManager = signInManager;
        }

        public void SignOut()
        {
            SignOutAsync().GetAwaiter().GetResult();
        }

        public Task SignOutAsync()
        {
            return _signInManager.SignOutAsync();
        }
    }
}
