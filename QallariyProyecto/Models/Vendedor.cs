using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QallariyProyecto.Models
{
    public class Vendedor
    {
            public int idVendedor { get; set; }
            public string nombres { get; set; }
            public string apellidos { get; set; }
            public string correo { get; set; }
            public int numeroDocumento { get; set; }
            public string fechaNacimiento { get; set; }
            public string telefono { get; set; }
            public string password { get; set; }
            public int idTipDoc { get; set; }
            public string descTipDoc { get; set; }
    }
}
