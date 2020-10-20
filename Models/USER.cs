using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto1.Models
{
    public class USER
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime fecha { get; set; }
        public int idRol { get; set; }
        public int IdUser { get; set; }
    }
}