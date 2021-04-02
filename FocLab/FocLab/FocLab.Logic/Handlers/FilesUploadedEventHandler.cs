using Croco.Core.Contract.Application;
using Croco.Core.EventSourcing.Implementations;
using Croco.Core.Logic.Files.Events;
using Microsoft.Extensions.Logging;
using FocLab.Logic.Services.External;
using System.Threading.Tasks;

namespace FocLab.Logic.EventHandlers
{
    public class FilesUploadedEventHandler : CrocoMessageHandler<FilesUploadedEvent>
    {
        FocLabFileService EccFileService { get; }

        public FilesUploadedEventHandler(ICrocoApplication application,
            ILogger<FilesUploadedEventHandler> logger,
            FocLabFileService eccFileService
            ) : base(application, logger)
        {
            EccFileService = eccFileService;
        }

        public override Task HandleMessage(FilesUploadedEvent model)
        {
            return EccFileService.CreateFiles(model.FileIds);
        }
    }
}