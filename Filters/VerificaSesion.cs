using Proyecto1.Controllers;
using Proyecto1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto1.Filters
{
    public class VerificaSession : ActionFilterAttribute
    {
        private USER USERR;
        private usuario oUsuario;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);

                // oUsuario = (usuario)HttpContext.Current.Session["User"];
                USERR = (USER)HttpContext.Current.Session["User"];
                if (USERR == null)
                {
                    if (filterContext.Controller is AccesoController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Acceso/Login");
                    }
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Acceso/Login");
            }

        }
    }
}