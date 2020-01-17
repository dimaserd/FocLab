using System.Collections.Generic;
using System.Linq;
using Croco.Core.Documentation.Models;
using Croco.Core.Documentation.Services;
using FocLab.Api.Controllers.Base;
using FocLab.Logic.Services;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Api.Controllers.Api.Developer
{
    /// <inheritdoc />
    /// <summary>
    /// Предоставляет автогенерируемую документацию
    /// </summary>
    [Route("Api/Documentation")]
    public class DocumentationController : BaseApiController
    {
        /// <inheritdoc />
        public DocumentationController(ChemistryDbContext context, ApplicationSignInManager signInManager, ApplicationUserManager userManager, IHttpContextAccessor httpContextAccessor) : base(context, signInManager, userManager, httpContextAccessor)
        {
        }

        /// <summary>
        /// Получить документацию по SignalR хабам
        /// </summary>
        /// <returns></returns>
        [HttpPost("Class")]
        [ProducesDefaultResponseType(typeof(CrocoTypeDescription))]
        public CrocoTypeDescription GetTypeDocumentation(string typeName)
        {
            if (typeName == null)
            {
                return null;
            }

            var type = CrocoTypeSearcher.FindType(typeName);

            if (type == null)
            {
                return null;
            }

            return CrocoTypeDescription.GetDescription(type);
        }
    }
}