using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto1.Models
{
    public class ventas
    {
        public List<ventas_Result> Retornoventas()
        {
            List<ventas_Result> venta = new List<ventas_Result>();
            using (var db=new restaurantEntities())
            {
                venta = db.ventas().ToList();
            }
            return venta;
        }
        public List<detalleboleta> Detallesbol()
        {
            List<detalleboleta> detalle = new List<detalleboleta>();
            using (var db = new restaurantEntities())
            {
                detalle = (from tt in db.detalleboleta
                           select tt).ToList();
            }
            return detalle;
        }
        public bool EliminarPedido(int id)
        {
            try
            {
                using (var db = new restaurantEntities())
                {
                    productopedido prodped = new productopedido();
                    var prodped1 = (from ta in db.productopedido
                                    where ta.idpedido == id
                                    select ta).ToList();
                    foreach (var item in prodped1)
                    {
                        db.productopedido.Remove(item);
                    }
                    db.SaveChanges();
                    var detalleb = (from dt in db.detalleboleta
                                    where dt.idpedido == id
                                    select dt).FirstOrDefault();
                    db.detalleboleta.Remove(detalleb);
                    db.SaveChanges();
                    db.pedido.Remove(db.pedido.Find(id));
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }     
           
        }
    }
}