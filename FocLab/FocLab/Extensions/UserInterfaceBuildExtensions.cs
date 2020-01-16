using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using Zoo.GenericUserInterface.Models;

namespace FocLab.Extensions
{
    public static class UserInterfaceBuildExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="model"></param>
        /// <param name="formDrawKey">Ключ для реализации интерфейса</param>
        /// <returns></returns>
        public static Task<IHtmlContent> RenderGenericUserInterfaceAsync(this IHtmlHelper htmlHelper, GenerateGenericUserInterfaceModel model, string formDrawKey = null)
        {
            htmlHelper.ViewData["formDrawKey"] = formDrawKey;
            return htmlHelper.PartialAsync("~/Views/Components/GenericUserInterface.cshtml", model);
        }

        public static Task<IHtmlContent> RenderGenericUserInterfaceAsync(this IHtmlHelper htmlHelper, IHaveGenericUserInterface model, string modelPrefix, GenericUserInterfaceValueProvider valueProvider)
        {
            return RenderGenericUserInterfaceAsync(htmlHelper, GenerateGenericUserInterfaceModel.Create(model, valueProvider, modelPrefix));
        }
    }
}
