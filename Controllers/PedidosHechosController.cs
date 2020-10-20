using Proyecto1.Filters;
using Proyecto1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto1.Controllers
{
    public class PedidosHechosController : Controller
    {
        ventas ventas = new ventas();

        [AuthorizeUser(idOperacion: 2)]
        public ActionResult Ventas()
        {
            return View();
        }
        [HttpPost]
        public JsonResult VentasRetorno()
        {
            List<ventas_Result> ventapedidos = ventas.Retornoventas();
            return Json(ventapedidos, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public void Eliminar(int id)
        {
            ventas.EliminarPedido(id);
        }
        
    }
}