using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITLAManage.Controllers
{
    [HandleError]
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ErrorHTTP404()
        {
            return View();
        }

        public ActionResult ErrorHTTP405()
        {
            return View();
        }

        public ActionResult ErrorHTTP500()
        {
            return View();
        }

        public ActionResult General()
        {
            return View();
        }

    }
}