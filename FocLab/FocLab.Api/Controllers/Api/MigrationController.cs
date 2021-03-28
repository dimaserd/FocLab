using Microsoft.AspNetCore.Mvc;
using MigrationTool.Tools;
using System.Threading.Tasks;

namespace FocLab.Api.Controllers.Api
{
    [Route("Api/Migration")]
    public class MigrationController : Controller
    {
        ExportOldTmsDataToTmsService ExportOldTmsDataToTmsService { get; }
        ExportDayTaskToOldTmsService ExportDayTaskToOldTmsService { get; }

        public MigrationController(ExportOldTmsDataToTmsService exportOldTmsDataToTmsService,
            ExportDayTaskToOldTmsService exportDayTaskToOldTmsService)
        {
            ExportOldTmsDataToTmsService = exportOldTmsDataToTmsService;
            ExportDayTaskToOldTmsService = exportDayTaskToOldTmsService;
        }

        
        [HttpGet("ExportOldTmsDataToTmsService/GetState")]
        public Task<ExportOldTmsDataToTmsService.CurrentState> ExportOldTmsDataToTmsService_GetState()
        {
            return ExportOldTmsDataToTmsService.GetState();
        }

        [HttpPost("ExportOldTmsDataToTmsService/PasteData")]
        public Task<int> ExportOldTmsDataToTmsService_PasteData()
        {
            return ExportOldTmsDataToTmsService.PasteData();
        }

        [HttpGet("ExportDayTaskToOldTmsService/GetState")]
        public Task<ExportDayTaskToOldTmsService.CurrentExportState> ExportDayTaskToOldTmsService_GetState()
        {
            return ExportDayTaskToOldTmsService.GetState();
        }

        [HttpPost("ExportDayTaskToOldTmsService/PasteData")]
        public Task<int> ExportDayTaskToOldTmsService_PasteData()
        {
            return ExportDayTaskToOldTmsService.PasteData();
        }
    }
}