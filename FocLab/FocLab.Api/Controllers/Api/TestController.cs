using System.Collections.Generic;
using System.Linq;
using Croco.Core.Contract;
using FocLab.Api.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Api.Controllers.Api
{
    /// <inheritdoc />
    /// <summary>
    /// Тестовый апи-контроллер для интеграционного тестирования методов
    /// </summary>
    [Route("Api/Test")]
    public class TestController : BaseApiController
    {
        /// <inheritdoc />
        public TestController(ICrocoRequestContextAccessor requestContextAccessor) : base(requestContextAccessor)
        {
        }

        /// <summary>
        /// Метод прокси тест
        /// </summary>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(List<int>))]
        [HttpPost("Proxy")]
        public List<int> ProxyTest()
        {
            //var res = ProxyHelper.Product.ProductGroups.GetProxiedList();

            var res = new int[3];

            var list = res.ToList();

            return list;
        }
    }
}
