﻿using System.Collections.Generic;
using Croco.Core.Logic.Models.Documentation;
using Croco.Core.Logic.Workers.Documentation;
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
        public CrocoTypeDescription GetTypeDocumentations(string typeName)
        {
            return ClassModelDescriptor.GetDocumentationForClass(typeName);
        }

        /// <summary>
        /// Получить документацию по SignalR хабам
        /// </summary>
        /// <returns></returns>
        [HttpPost("JsonExample")]
        [ProducesDefaultResponseType(typeof(string))]
        public string GetJson(string typeName)
        {
            return ClassModelDescriptor.GetJsonExample(typeName);
        }

        /// <summary>
        /// Поиск типов
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        [HttpPost("SearchTypes")]
        [ProducesDefaultResponseType(typeof(List<string>))]
        public List<string> SearchTypes(string typeName)
        {
            return ClassModelDescriptor.SearchClassTypes(typeName);
        }

    }
}
