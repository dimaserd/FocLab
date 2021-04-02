using Clt.Contract.Events;
using Clt.Model;
using Clt.Model.Entities;
using Croco.Core.Contract.Application;
using Croco.Core.EventSourcing.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using FocLab.Logic.Commands;
using FocLab.Logic.Models.Users;

namespace FocLab.App.Logic.Handlers
{
    public class ClientDataUpdatedEventHandler : CrocoMessageHandler<ClientDataUpdatedEvent>
    {
        public ClientDataUpdatedEventHandler(ICrocoApplication application, ILogger<ClientDataUpdatedEventHandler> logger) : base(application, logger)
        {
        }

        public override Task HandleMessage(ClientDataUpdatedEvent model)
        {
            return GetSystemTransactionHandler().ExecuteAndCloseTransaction(async amb =>
            {
                var dataCon = amb.GetAmbientContext<CltDbContext>()
                    .DataConnection;

                var client = await dataCon
                    .GetRepositoryFactory()
                    .Query<Client>()
                    .FirstOrDefaultAsync(x => x.Id == model.Id);

                await Application.EventSourcer.PublishAsync(dataCon.RequestContext, new UpdateUserCommand
                {
                    User = new UserModelBase
                    {
                        Id = client.Id,
                        Email = client.Email,
                        Name = client.Name
                    }
                });
            });
        }
    }
}
