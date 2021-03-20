using System.Threading.Tasks;

namespace FocLab.Logic.Abstractions
{
    public interface IApplicationAuthenticationManager
    {
        Task SignOutAsync();
    }
}