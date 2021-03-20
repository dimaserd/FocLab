using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Api.Controllers.Api
{
    /// <inheritdoc />
    /// <summary>
    /// Тестовый апи-контроллер для интеграционного тестирования методов
    /// </summary>
    [Route("Api/Test")]
    public class TestController : Controller
    {
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