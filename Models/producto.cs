//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public producto()
        {
            this.tipoproducto = new HashSet<tipoproducto>();
            this.promocion = new HashSet<promocion>();
            this.productopedido = new HashSet<productopedido>();
        }
    
        public int id { get; set; }
        public string nombre { get; set; }
        public Nullable<decimal> precio { get; set; }
        public Nullable<int> stock { get; set; }
        public byte[] imagen { get; set; }
        public Nullable<int> ocultar { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tipoproducto> tipoproducto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<promocion> promocion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<productopedido> productopedido { get; set; }
    }
}
