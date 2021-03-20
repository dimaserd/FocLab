using Croco.Core.Documentation.Models;
using Croco.Core.Documentation.Services;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Api.Controllers.Api.Developer
{
    /// <inheritdoc />
    /// <summary>
    /// Предоставляет автогенерируемую документацию
    /// </summary>
    [Route("Api/Documentation")]
    public class DocumentationController : Controller
    {
        /// <summary>
        /// Получить документацию по классу
        /// </summary>
        /// <returns></returns>
        [HttpPost("Class")]
        [ProducesDefaultResponseType(typeof(CrocoTypeDescriptionResult))]
        public CrocoTypeDescriptionResult GetTypeDocumentation(string typeName)
        {
            if (typeName == null)
            {
                return null;
            }

            var type = CrocoTypeSearcher.FindFirstTypeByName(typeName);

            if (type == null)
            {
                return null;
            }

            return CrocoTypeDescription.GetDescription(type);
        }
    }
}