using Croco.Core.Contract.Application;
using Croco.Core.EventSourcing.Implementations;
using Microsoft.Extensions.Logging;
using NewFocLab.Logic.Commands;
using NewFocLab.Logic.Services.External;
using System.Threading.Tasks;

namespace NewFocLab.Logic.EventHandlers
{
    /// <summary>
    /// Обработчик для команды создания пользователя
    /// </summary>
    public class CreateUserCommandHandler : CrocoMessageHandler<CreateUserCommand>
    {
        UserService UserService { get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="application"></param>
        /// <param name="logger"></param>
        /// <param name="userService"></param>
        public CreateUserCommandHandler(ICrocoApplication application,
            ILogger<CreateUserCommandHandler> logger,
            UserService userService) : base(application, logger)
        {
            UserService = userService;
        }

        /// <summary>
        /// Обработать сообщение
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override Task HandleMessage(CreateUserCommand model)
        {
            return UserService.CreateUser(model.User);
        }
    }
}