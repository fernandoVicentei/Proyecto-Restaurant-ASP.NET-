using Proyecto1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(int User, string Pass)
        {
            try
            {
                using (restaurantEntities db = new restaurantEntities())
                {
                    //verificamos que sea un cliente
                    var oUser = (from d in db.cliente
                                 where d.telefono.Value == User && d.contraseña == Pass.Trim()  
                                 select d).FirstOrDefault();                    
                    if (oUser != null)
                    {
                        Session["User"] = oUser;
                        // 1 de cliente
                        Session["Tipo"] = 1;
                    }
                    else
                    {
                        var toUser = (from a in db.empleado
                                      where a.telefooo == User && a.contrasenia == Pass.Trim()
                                      select a).FirstOrDefault();
                        if (toUser != null)
                        {
                            Session["Empleado"] = toUser;
                            //dos de empleado
                            Session["Tipo"] = 2;
                        }
                        else
                        {
                            ViewBag.Error = "Usuario o contraseña invalida";
                            return View();
                        }
                    }
                }
                /* */
                return RedirectToAction("Catalogo", "Productos");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }

        }
    }
}