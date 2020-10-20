using Proyecto1.Filters;
using Proyecto1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto1.Controllers
{
    public class ControlUsuarioController : Controller
    {
        Empleados emple = new Empleados();
        // GET: ControlUsuario
        [AuthorizeUser(idOperacion: 14)]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult RetornarEmpleados()
        {
            var enviar = emple.EmpleaReturn();
            return Json(enviar, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        /*
         , { nombre: this.nombre, contra: this.contra, telefono: this.telef,rol:this.tipo }
         string nombre,string contra,int telefono,int rol*/
        public void AgregarNuevo(string nombre, string contra, int telefono, int rol)
        {                
            emple.Agregar(nombre,contra,telefono,rol);            
        }
        public void Editar(string nombre, string contra, int telefono, int rol, int id)
        {
            emple.Editar(nombre, contra, telefono, rol, id);
        }
        public void EliminarU(int id)
        {
            emple.EliminarU(id);
        }
    }
}