using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QallariyProyecto.Models
{
    public class Producto
    {
        public string idProducto { get; set; }
        public string idnegocio { get; set; }
        public string negocio { get; set; }
        public string descripcion { get; set; }
        public Decimal precio { get; set; }
        public int stock { get; set; }
        public int visitas { get; set; }
        public int meInteresa { get; set; }
        public int idestado { get; set; }
        public string estado { get; set; }
        public byte[] imagen1 { get; set; }
        public byte[] imagen2 { get; set; }
        public byte[] imagen3 { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_modificacion { get; set; }

    }
}
