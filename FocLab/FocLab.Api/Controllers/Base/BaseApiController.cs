using Croco.Core.Contract;
using Croco.Core.Contract.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.StaticFiles;

namespace FocLab.Api.Controllers.Base
{
    /// <inheritdoc />
    /// <summary>
    /// Базовый абстрактный контроллер в котором собраны общие методы и свойства
    /// </summary>
    public class BaseApiController : Controller
    {
        ICrocoRequestContext RequestContext { get; }

        public BaseApiController(ICrocoRequestContextAccessor requestContextAccessor, IActionContextAccessor actionContextAccessor)
        {
            RequestContext = requestContextAccessor.GetRequestContext();
            UserId = RequestContext.UserPrincipal.UserId;
            var actionContext = actionContextAccessor.ActionContext;
        }

        public string UserId { get; }
        public IActionContextAccessor ActionContextAccessor { get; }

        public static string GetMimeMapping(string fileName)
        {
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var contentType);

            return contentType ?? "application/octet-stream";
        }

        /// <summary>
        /// Возвращает физический файл по пути и имени файла, из имени файла берется Mime тип
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected PhysicalFileResult PhysicalFileWithMimeType(string filePath, string fileName)
        {
            return PhysicalFile(filePath, GetMimeMapping(fileName), fileName);
        }
    }
}