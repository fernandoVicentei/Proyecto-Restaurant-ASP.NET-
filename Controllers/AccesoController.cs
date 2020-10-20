using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto1.Models;

namespace Proyecto1.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Login()
        {
            Session["User"] = null;
            return View();
        }
        USER userr = new USER();
        
        [HttpPost]
        public ActionResult Login(int User, string Pass)
        {
            try
            {
                using (restaurantEntities db = new restaurantEntities())
                {
                    empleado emple;
                    int id = 0;
                    int idUsuario = 0;
                    var userInterno = (from dd in db.cliente
                                       where dd.telefono.Value == User && dd.contraseña == Pass.Trim()
                                       select dd).FirstOrDefault();
                    if (userInterno == null)
                    {
                        emple = (from d2 in db.empleado
                                       where d2.telefooo == User && d2.contrasenia == Pass.Trim()
                                       select d2).FirstOrDefault();
                        if (emple==null)
                        {
                            ViewBag.Error = "Usuario o contraseña invalida";
                            return View();
                        }id = (int)emple.rolEmp;
                        idUsuario = (int)emple.id;
                    }
                    else
                    {
                        idUsuario = (int)userInterno.id;
                        id =(int) userInterno.rolCli;
                    }


                    /*var oUser = (from d in db.usuario
                                 where d.email == User.Trim() && d.password == Pass.Trim()
                                 select d).FirstOrDefault();*/
                    var oUser = (from d in db.usuario
                                 where d.id==id
                                 select d).FirstOrDefault();
                    userr.id = oUser.id;                    
                    userr.email= oUser.email;
                    userr.idRol =(int) oUser.idRol;
                    userr.nombre = oUser.nombre;
                    userr.password = oUser.password;
                    userr.IdUser = idUsuario;
                    Session["User"] =userr;
                }

                return RedirectToAction("Catalogo", "Productos");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }

        }
        public ActionResult Nuevo()
        {
            try
            {
                string nombre = Request.Form["User"].ToString();
                int telefono = int.Parse(Request.Form["telefono"].ToString());
                string ubicacion = Request.Form["ubicacion"].ToString();
                string contrasenia = Request.Form["password"].ToString();

                using (var db=new restaurantEntities())
                {
                    var d = new cliente();
                    d.nombrecompleto = nombre;
                    d.telefono = telefono;
                    d.ubicacion = ubicacion;
                    d.contraseña = contrasenia;
                    d.rolCli = 3;
                    db.cliente.Add(d);
                    db.SaveChanges();

                    userr.id =3;
                    userr.email = "cliente@gmail.com";
                    userr.idRol = 3;
                    userr.nombre = d.nombrecompleto;
                    userr.password =d.contraseña;
                    userr.IdUser = d.id;
                    Session["User"] = userr;
                   
                }                
                return RedirectToAction("Catalogo", "Productos");
            }
            catch (Exception w)
            {
                ViewBag.Error = w.Message;
                return View();
            }
        }
    }
}