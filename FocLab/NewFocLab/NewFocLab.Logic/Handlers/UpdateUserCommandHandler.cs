using Croco.Core.Contract.Application;
using Croco.Core.EventSourcing.Implementations;
using Microsoft.Extensions.Logging;
using NewFocLab.Logic.Commands;
using NewFocLab.Logic.Services.External;
using System.Threading.Tasks;

namespace NewFocLab.Logic.EventHandlers
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