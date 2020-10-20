using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto1.Models
{
    public class Ppromocion
    {
        public int cantidad { get; set; }
        public int idproducto { get; set; }
        public string titulo { get; set; }
        public decimal nuevoprecio { get; set; }
        public string img { get; set; }
        public int id { get; set; }
        public string descripcion { get; set; }
        public void AgregarPromo(int idPP, string tituloo, int cantidadd, decimal precioneww, string imageSrcc,string descripcion)
        {
            try
            {
                using (var agr = new restaurantEntities())
                {
                    promocion prom = new promocion();
                    prom.cantidad = cantidadd;
                    prom.idproducto = idPP;
                    prom.titulo = tituloo;
                    prom.nuevoprecio = precioneww;
                    prom.img = imageSrcc;
                    prom.descripPromo = descripcion;
                    agr.promocion.Add(prom);
                    agr.SaveChanges();
                }
            }
            catch (Exception ee)
            {
                throw;
            }           
        }
        public List<promocion> ReturnRegistros()
        {            
            using (var db= new restaurantEntities())
            {
                var lst = (from d in db.promocion
                           select d).ToList();
                return lst;
            }    
        }
        public void EliminarPromocion(int id)
        {
            try
            {
                using (var db = new restaurantEntities())
                {
                    var s = (from e in db.promocion
                             where e.id == id
                             select e).FirstOrDefault<promocion>();
                    db.promocion.Remove(s);
                    db.SaveChanges();
                }
            }
            catch (Exception w)
            {               
            }
           
        }
    }
}