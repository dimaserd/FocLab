using Croco.Core.Abstractions;
using Croco.Core.EventSourcing.Abstractions;
using Croco.Core.EventSourcing.Enumerations;
using System.Threading.Tasks;

namespace FocLab.Implementations
{
    public class DatabaseCrocoMessageStateHandler : ICrocoMessageStateHandler
    {
        public Task<CrocoMessageState> GetMessageStateAsync(string messageId)
        {
            return Task.FromResult(CrocoMessageState.Created);
        }

        public Task UpdateMessageStateAsync(ICrocoAmbientContext ambientContext, CrocoMessageState state)
        {
            return Task.CompletedTask;
        }
    }
}
