using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto1.Models
{
    public class Carrito
    {
    
        public int idproducto { get; set; }
        public string nombre { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public int tipoPedido { get; set; }
        public string img { get; set; }
        public string descripcion { get; set; }
        public int idRol { get; set; }
    }
}