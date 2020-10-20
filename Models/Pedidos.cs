using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto1.Models
{
    public class Pedidos
    {
        public void AgregarNuevos(List<Carrito> pedidos, int modopago)
        {
            int idpedido = 0;
            decimal total = 0;
            string detalles = "Los productos que se pidieron son ";
            //concatenamos los nombres de los productos
            for (int i = 0; i < pedidos.Count; i++)
            {
                detalles += " " + pedidos[i].nombre+" ,";
            }
            using (var db= new restaurantEntities())
            {
                pedido newpedido = new pedido();
                newpedido.idcliente =1;
                // (idcliente!=0 ? "PEDIDO HECHO POR UN EMPLEADO : "PEDIDO HECHO POR UN CLIENTE"")
                newpedido.detalle = detalles;
                db.pedido.Add(newpedido);
                db.SaveChanges();
                idpedido = newpedido.id;
                productopedido prodpedido = new productopedido();
                foreach (var item in pedidos)
                {                   
                    total += (item.cantidad * item.precio);
                    prodpedido.idpedido = idpedido;
                    prodpedido.idproducto = item.idproducto;
                    prodpedido.cantidad = item.cantidad;
                    prodpedido.tipopedido = item.tipoPedido;
                    db.productopedido.Add(prodpedido);
                    db.SaveChanges();
                }               
            }
            Detalleboleta(idpedido, modopago,1,total);
        }
        private void Detalleboleta(int idpedidoProd,int modopago,int idempleado,decimal totalpago)
        {
            using (var db =new restaurantEntities())
            {
                detalleboleta detalle = new detalleboleta();
                if ( idempleado!=0)
                {                    
                    detalle.idpedido = idpedidoProd;                    
                    detalle.idempleado = idempleado;
                    detalle.fecha = DateTime.Now;
                    detalle.montifinal = totalpago;
                    if (modopago == 7)
                    {
                        detalle.observacion = "Pago que se efectuo por tarjeta de credito o paypal";
                        detalle.idmodopago = 7;
                    }
                    else
                    {
                        detalle.observacion = "Pago realizado al momento de la entrega del repartidor";
                        detalle.idmodopago = 8;
                    }
                }
                else
                {
                    //aca poner la observacion generada desde la computadora
                }
                db.detalleboleta.Add(detalle);
                db.SaveChanges();
            }

        }
    }
}