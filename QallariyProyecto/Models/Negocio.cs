using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QallariyProyecto.Models
{
    public class Negocio
    {
        public string idNegocio { get; set; }
        public string nombreNegocio { get; set; }
        public string descripcionNegocio { get; set; }
        public int vendedor { get; set; }
        public string categoria { get; set; }
        public int distrito { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public byte[] imagen { get; set; }
        public string facebook { get; set; }
        public string instagram { get; set; }
        public string tiktok { get; set; }
        public string correo { get; set; }
        public string whatsapp { get; set; }
        public string website { get; set; }
        public string verificado { get; set; }
        public bool hasImage { get; set; }
    }
    public class NegocioListar
    {
        public string id { get;  set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string nom_vendedor { get; set; }
        public string desc_categoria { get; set; }
        public string direccion { get; set; }
        public byte[] imagen { get; set; } 
    }

    public class NegocioListado
    {
        public string idNegocio { get; set; }
        public string nombreNegocio { get; set; }
        public string descripcionNegocio { get; set; }
        public int vendedor { get; set; }
        public string categoria { get; set; }
        public int distrito { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public byte[] imagen { get; set; }
        public string facebook { get; set; }
        public string instagram { get; set; }
        public string tiktok { get; set; }
        public string correo { get; set; }
        public string whatsapp { get; set; }
        public string website { get; set; }
        public string nombrevendedor { get; set; }
        public string apellidovendedor { get; set; }
        public string descripcionvendedor { get; set; }
        public int numerodocvendedor { get; set; }
        public string descdistrito { get; set; }
        public string descprov { get; set; }
        public int iddepart { get; set; }
        public string descdepart { get; set; }
    }

    public class NegocioUpload
    {
        public string nombreNegocio { get; set; }
        public string descripcionNegocio { get; set; }
        public int vendedor { get; set; }
        public int categoria { get; set; }
        public int distrito { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public byte[] imagen { get; set; }
    }
}
