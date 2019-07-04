using Microsoft.AspNetCore.Mvc;

namespace FocLab.Areas.Chemistry.Controllers.Mvc
{
    public class MethodsController : Controller
    {
        // GET: Methods
        public ActionResult Index()
        {
            return View();
        }

        // GET: Methods/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Methods/Create
        public ActionResult Create()
        {
            return View();
        }
    }
}