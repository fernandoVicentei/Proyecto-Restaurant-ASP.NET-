using Proyecto1.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto1.Controllers
{
    public class PrivadoController : Controller
    {
        // GET: Privado

        [AuthorizeUser(idOperacion: 13)]
        public ActionResult Index()
        {
            return View();
        }
    }
}