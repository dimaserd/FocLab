using Croco.Core.Contract.Application;
using Croco.Core.EventSourcing.Implementations;
using Microsoft.Extensions.Logging;
using FocLab.Logic.Commands;
using FocLab.Logic.Services.External;
using System.Threading.Tasks;

namespace FocLab.Logic.EventHandlers
{
    public class UpdateUserCommandHandler : CrocoMessageHandler<UpdateUserCommand>
    {
        UserService UserService { get; }

        public UpdateUserCommandHandler(ICrocoApplication application,
            ILogger<UpdateUserCommandHandler> logger,
            UserService userService) : base(application, logger)
        {
            UserService = userService;
        }

        public override Task HandleMessage(UpdateUserCommand model)
        {
            return UserService.UpdateUser(model.User);
        }
    }
}