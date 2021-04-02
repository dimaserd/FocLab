using Croco.Core.Contract.Models.Search;
using Croco.Core.Utils;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace FocLab.Extensions
{
    public static class HtmlExtensions
    {
        public static IHtmlContent GetJson(this IHtmlHelper htmlHelper, object obj)
            => new HtmlString(Tool.JsonConverter.Serialize(obj));

        public static Task<IHtmlContent> RenderPaginationAsync<T>(this IHtmlHelper htmlHelper, GetListResult<T> model, string linkFormat) where T : class
        {
            return htmlHelper.PartialAsync("~/Views/Components/Pagination.cshtml", PagerModel.ToPagerModel(model, linkFormat));
        }

        public static Task<IHtmlContent> RenderApplicationFilesAsync(this IHtmlHelper htmlHelper, string applicationName)
        {
            return htmlHelper.PartialAsync("~/Views/Helpers/RenderApplicationFiles.cshtml", applicationName);
        }
    }
}