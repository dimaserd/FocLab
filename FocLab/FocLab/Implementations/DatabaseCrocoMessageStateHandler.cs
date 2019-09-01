using Croco.Core.Abstractions;
using Croco.Core.EventSourcing.Abstractions;
using Croco.Core.EventSourcing.Enumerations;
using Croco.Core.EventSourcing.Implementations;
using System.Threading.Tasks;

namespace FocLab.Implementations
{
    public class CrocoMessage
    {
        public string Id { get; set; }

        public CrocoMessageState State { get; set; }

        public string ObjectJson { get; set; }
    }

    public class DatabaseCrocoMessageStateHandler : ICrocoMessageStateHandler
    {
        public Task<CrocoMessageState> GetMessageStateAsync(string messageId)
        {
            return Task.FromResult(CrocoMessageState.Created);
        }


        public Task UpdateMessageStateAsync(ICrocoAmbientContext ambientContext, CrocoIntegrationMessage message, CrocoMessageState state)
        {
            return Task.CompletedTask;
        }
    }
}
