using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto1.Models
{
    public class Empleados
    {
        public int id { get; set; }
        public string Nombrecompleto { get; set; }
        public string contrasenia { get; set; }
        public int telefono { get; set; }
        public int rolEmp { get; set; }
        public List<empleado> EmpleaReturn()
        {
            List<empleado> emple = new List<empleado>();
            using ( var db= new restaurantEntities())
            {
                emple = (from tt in db.empleado
                           select tt).ToList();
            }
            return emple;
        }
        public bool Agregar(string nombre, string contra, int telefono, int rol)
        {
            try
            {
                using (var db = new restaurantEntities())
                {
                    empleado nuevo = new empleado();
                    nuevo.contrasenia = contra;
                    nuevo.Nombrecompleto = nombre;
                    nuevo.telefooo = telefono;
                    nuevo.rolEmp = rol;
                    db.empleado.Add(nuevo);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw;
            }            
        }
        public void Editar(string nombre, string contra, int telefono, int rol,int id)
        {
            try
            {
                using (var db = new restaurantEntities())
                {
                    var nuevo = db.empleado.Find(id);                    
                    nuevo.contrasenia = contra;
                    nuevo.Nombrecompleto = nombre;
                    nuevo.telefooo = telefono;
                    nuevo.rolEmp = rol;                    
                    db.SaveChanges();
                }
                
            }
            catch (Exception e)
            {               
                throw;
            }
        }

        public void EliminarU(int id)
        {
            using (var db= new restaurantEntities())
            {
                var borrar = db.empleado.Find(id);
                if (borrar!=null)
                {
                    db.empleado.Remove(borrar);
                }
                db.SaveChanges();
            }
        }
    }
}
