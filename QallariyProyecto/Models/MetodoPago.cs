using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QallariyProyecto.Models
{
    public class MetodoPago
    {
        public int idMetPag { get; set; }
        public string titular { get; set; }
        public string tipo { get; set; }
        public string numero { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public string codigo { get; set; }
        public Vendedor vendedor { get; set; }  }
    }

