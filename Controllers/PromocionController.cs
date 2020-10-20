using Proyecto1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto1.Controllers
{
    public class PromocionController : Controller
    {
        // GET: Promocion
        Ppromocion promo = new Ppromocion();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public void Agregar()
        {                        
            string titulo = this.HttpContext.Request.Form["titulo"];
            int idP = int.Parse(this.HttpContext.Request.Form["idP"]);
            int cantidad = int.Parse(this.HttpContext.Request.Form["cantidad"].ToString());
            decimal precio = decimal.Parse(this.HttpContext.Request.Form["precio"].ToString());
            // Nombre de la imagen
            HttpPostedFileBase filea = this.HttpContext.Request.Files.Get("imagen");
            string nombre = filea.FileName.Substring(0, filea.FileName.LastIndexOf("."));
            string descripcion = this.HttpContext.Request.Form["descripcion"].ToString();
            string contentType = filea.ContentType;
            // Imagen convertida a arreglo de bytes
            byte[] imagen = new byte[filea.InputStream.Length];
            filea.InputStream.Read(imagen, 0, imagen.Length);
            string imageBase64 = Convert.ToBase64String(imagen);
            string imageSrc = string.Format("data:image/jpeg;base64,{0}", imageBase64);
            promo.AgregarPromo( idP, titulo,  cantidad,  precio, imageSrc,descripcion);
        }
        public JsonResult RecojerPromociones()
        {
            List<promocion> prom = promo.ReturnRegistros();
            return Json(prom, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public void Eliminar(int id)
        {
            promo.EliminarPromocion(id);
        }
    }
}
