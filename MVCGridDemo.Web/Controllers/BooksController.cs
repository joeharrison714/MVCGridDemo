using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCGridDemo.Web.Controllers
{
    public class BooksController : Controller
    {
        // GET: Demo
        public ActionResult Index()
        {
            return View();
        }
    }
}