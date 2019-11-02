using Croco.Core.Abstractions;
using Croco.Core.Abstractions.EventSourcing;
using Croco.Core.Abstractions.EventSourcing.Enumerations;
using Croco.Core.Abstractions.EventSourcing.Models;
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

        public Task<CrocoMessageState> GetMessageStateAsync(ICrocoAmbientContext ambientContext, string messageId)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateMessageStateAsync(ICrocoAmbientContext ambientContext, CrocoIntegrationMessage message, CrocoMessageState state)
        {
            return Task.CompletedTask;
        }
    }
}
