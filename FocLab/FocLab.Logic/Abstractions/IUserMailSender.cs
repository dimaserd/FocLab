using System.Threading.Tasks;

namespace FocLab.Logic.Abstractions
{
    public interface IUserMailSender
    {
        Task SendMailUnSafeAsync(SendMailMessage model);
    }
}