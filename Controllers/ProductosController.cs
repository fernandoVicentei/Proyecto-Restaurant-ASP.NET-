using Proyecto1.Filters;
using Proyecto1.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Proyecto1.Controllers
{
    public class ProductosController : Controller
    {
        // GET: Productos
        Products pro = new Products();
        [AuthorizeUser(idOperacion: 1)]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetProductos()
        {
            List<Returnproducto_Result> ls = new Products().Retornar();
            int aux = 0;
            List<object> d = new List<object>();
            foreach (var item in ls)
            {
                if (item.imagen != null)
                {
                    string imageBase64 = Convert.ToBase64String(item.imagen);
                    string imageSrc = string.Format("data:image/jpeg;base64,{0}", imageBase64);
                    ls[aux].pIm = imageSrc;
                    ls[aux].imagen = null;
                }
                aux++;
            }         
           return Json(ls,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public void Principal()
        {
            int opcion = int.Parse(this.HttpContext.Request.Form["opcion"].ToString());
            HttpPostedFileBase filea = this.HttpContext.Request.Files.Get("imagen");
            string nombreP = this.HttpContext.Request.Form["nombre"];
            string descripcion = this.HttpContext.Request.Form["descripcion"].ToString();
            int tipo = int.Parse(this.HttpContext.Request.Form["tipo"].ToString());
            int stock = int.Parse(this.HttpContext.Request.Form["stock"].ToString());
            decimal precio = decimal.Parse(this.HttpContext.Request.Form["precio"].ToString());
            int visible = int.Parse(this.HttpContext.Request.Form["visible"].ToString());
            // Nombre de la imagen
            string nombre = filea.FileName.Substring(0, filea.FileName.LastIndexOf("."));
            // Extensión del archivo
            string ext = nombre.Substring(nombre.LastIndexOf(".") + 1);
            // Tipo de contenido
            string contentType = filea.ContentType;
            // Imagen convertida a arreglo de bytes
            byte[] imagen = new byte[filea.InputStream.Length];
            filea.InputStream.Read(imagen, 0, imagen.Length);
            switch (opcion)
            {
                case 1:
                    //AGREGAR
                    this.AgregarP(nombreP,  precio,  stock,  imagen,tipo,  descripcion);
                    break;
                case 2:
                    //ACTUALIZAR
                    int id = int.Parse(this.HttpContext.Request.Form["id"].ToString());
                    this.Editar(id, nombreP, precio, stock, imagen, tipo, descripcion,visible);
                    break;                
                default:
                    break;
            }
        }
        public  void AgregarP(string nombreP,decimal precio,int stock,byte[] imagen ,int tipo,string descripcion)
        {
            
           /* HttpPostedFileBase filea = this.HttpContext.Request.Files.Get("imagen");
            string nombreP = this.HttpContext.Request.Form["nombre"];
            string descripcion = this.HttpContext.Request.Form["descripcion"].ToString();
            int tipo = int.Parse(this.HttpContext.Request.Form["tipo"].ToString());
            int stock = int.Parse(this.HttpContext.Request.Form["stock"].ToString());
            decimal precio = decimal.Parse(this.HttpContext.Request.Form["precio"].ToString());
            // Nombre de la imagen
            string nombre = filea.FileName.Substring(0, filea.FileName.LastIndexOf("."));
            // Extensión del archivo
            string ext = nombre.Substring(nombre.LastIndexOf(".") + 1);
            // Tipo de contenido
            string contentType = filea.ContentType;
            // Imagen convertida a arreglo de bytes
            byte[] imagen = new byte[filea.InputStream.Length];
            filea.InputStream.Read(imagen, 0, imagen.Length);    */       
            pro.Agregar(nombreP, precio, stock,imagen , tipo, descripcion);
        }
        public void EliminarP(int id)
        {
            pro.EliminarPP(id);
        }
        public void Editar(int id,string nombreP,decimal precio,int stock,byte[] imagen,int tipo,string descripcion, int visible)
        {
           /* HttpPostedFileBase filea = this.HttpContext.Request.Files.Get("imagen");
            string nombreP = this.HttpContext.Request.Form["nombre"];
            string descripcion = this.HttpContext.Request.Form["descripcion"].ToString();
            int tipo = int.Parse(this.HttpContext.Request.Form["tipo"].ToString());
            int stock = int.Parse(this.HttpContext.Request.Form["stock"].ToString());
            decimal precio = decimal.Parse(this.HttpContext.Request.Form["precio"].ToString());
            int id = int.Parse(this.HttpContext.Request.Form["id"].ToString());
            // Nombre de la imagen
            string nombre = filea.FileName.Substring(0, filea.FileName.LastIndexOf("."));
            // Extensión del archivo
            string ext = nombre.Substring(nombre.LastIndexOf(".") + 1);
            // Tipo de contenido
            string contentType = filea.ContentType;
            // Imagen convertida a arreglo de bytes
            byte[] imagen = new byte[filea.InputStream.Length];
            filea.InputStream.Read(imagen, 0, imagen.Length);*/
            pro.EditarP(id,nombreP,precio,stock,imagen,tipo,descripcion,visible);
        }
              
        public ActionResult Catalogo()
        {

            return View();
        }
       
    }
}
