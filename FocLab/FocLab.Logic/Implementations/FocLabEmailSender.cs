using FocLab.Logic.Abstractions;
using System.Threading.Tasks;

namespace FocLab.Logic.Implementations
{
    public class FocLabEmailSender : IUserMailSender
    {
        public Task SendMailUnSafeAsync(SendMailMessage model)
        {
            //TODO Implement EmailSender
            return Task.CompletedTask;
        }
    }
}
