using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace QallariyProyecto.Models
{
    public class DbImagen :DbContext
    {
        public DbSet<Negocio> Imagens { get; set; }
    }
}
