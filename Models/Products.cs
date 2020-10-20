using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Proyecto1.Models
{
    public class Products
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public double precio { get; set; }
        public int stock { get; set; }
        public byte imagen { get; set; }
        public int tipo { get; set; }
        public string descripcion { get; set; }

        public List<Returnproducto_Result> Retornar()
        {
            List<Returnproducto_Result> lis = new List<Returnproducto_Result> ();
            using (var prod=new restaurantEntities())
            {
                lis= prod.Returnproducto().ToList();
                return lis;
            }                       
        }
        //
        public void Agregar(string nombre,decimal precio,int stock,byte[] imagen,int tipoP,string descripcionP)
        {
            using(var agr=new restaurantEntities())
            {
                producto prd = new producto();
                prd.nombre = nombre;
                prd.precio = precio;
                prd.stock = stock;
                prd.imagen = imagen;
                agr.producto.Add(prd);
                int prs = prd.id;
                tipoproducto tip = new tipoproducto();
                tip.tipo = tipoP;
                tip.descripcion = descripcionP;
                tip.product = prs;
                agr.tipoproducto.Add(tip);
                agr.SaveChanges();               
             /*  
               */
            }
        }
        public bool EliminarPP(int id)
        {
            using (var agr=new restaurantEntities())
            {
                //eliminae los registros que dependan de esta                
                var s = (from tp in agr.tipoproducto
                      where tp.product.Value==id 
                       select tp  ).FirstOrDefault<tipoproducto>();                
                if (s == null)
                {
                    return false;
                }
                else
                {
                    agr.tipoproducto.Remove(s);
                    var idped = (from ta in agr.productopedido
                                 where ta.idproducto.Value==id
                                 select ta).ToList();
                    foreach (var item in idped)
                    {
                        agr.productopedido.Remove(item);
                       
                    }
                     agr.producto.Remove(agr.producto.Find(id));
                    agr.SaveChanges();
                    return true;
                }               
            }
        }
        public bool EditarP(int id, string nombre, decimal precio, int stock, byte[] imagen, int tipoP, string descripcionP, int visible) 
        {
            using (var agr=new restaurantEntities())
            {
                var s = (from tp in agr.tipoproducto
                         where tp.product.Value == id
                         select tp).FirstOrDefault<tipoproducto>();
                if (s != null)
                {
                    s.tipo = tipoP;
                    s.descripcion = descripcionP;
                    var producer = agr.producto.Find(id);
                    producer.nombre = nombre;
                    producer.precio = precio;
                    producer.stock = stock;
                    producer.imagen = imagen;
                    agr.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}