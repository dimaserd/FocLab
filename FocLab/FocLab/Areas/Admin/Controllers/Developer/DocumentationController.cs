using Microsoft.AspNetCore.Mvc;

namespace FocLab.Areas.Admin.Controllers.Developer
{
    [Area(Consts.AreaConsts.Admin)]
    public class DocumentationController : Controller
    {
        public IActionResult CodeGen()
        {
            return View();
        }
    }
}