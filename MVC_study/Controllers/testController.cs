using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_study.Controllers
{
    public class testController : Controller
    {
        // GET: test
        public ActionResult Index()
        {
            ViewBag.title = "test1";
            return View();
        }
    }
}