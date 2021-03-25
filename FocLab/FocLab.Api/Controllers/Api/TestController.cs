using Croco.Core.Contract.Health;
using Croco.Core.Health.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FocLab.Api.Controllers.Api
{
    /// <inheritdoc />
    /// <summary>
    /// Тестовый апи-контроллер для интеграционного тестирования методов
    /// </summary>
    [Route("Api/Test")]
    public class TestController : Controller
    {
        CrocoHealthCheckService HealthCheckService { get; }

        public TestController(CrocoHealthCheckService healthCheckService)
        {
            HealthCheckService = healthCheckService;
            
        }

        [HttpGet("CrocoApp/Health")]
        public Task<CrocoHealthCheckResult[]> GetCrocoAppHealth()
        {
            return HealthCheckService.CheckHealth();
        } 
    }
}