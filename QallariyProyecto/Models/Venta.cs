using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QallariyProyecto.Models
{
    public class Venta
    {
        public string idVenta { get; set; }
        public int vendedor { get; set; }
        public int membresia { get; set; }
        public int metodoPago { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaModificacion { get; set; }
        public DateTime fechaExpiracion { get; set; }
    }
}
