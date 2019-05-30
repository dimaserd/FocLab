using System.Threading.Tasks;

namespace FocLab.Logic.Abstractions
{
    public interface IApplicationAuthenticationManager
    {
        void SignOut();

        Task SignOutAsync();
    }
}
