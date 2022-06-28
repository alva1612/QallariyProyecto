using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using QallariyProyecto.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using QallariyProyecto.Models;
using Microsoft.AspNetCore.Session;

namespace QallariyProyecto.Controllers
{
    //HttpContext.Session.GetString(sesion, reg.correo);
  
   public class ProductoController : Controller
    {
        UtilsController uc = new UtilsController();
        LoginController lc = new LoginController();
     
        async Task<IEnumerable<Estado>> tipoestado()
        {
            List<Estado> temporal = new List<Estado>();
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("https://localhost:5001/api/Utils/");
                //recibir mensaje
                HttpResponseMessage mensaje = await cliente.GetAsync("estado");
                if (mensaje.IsSuccessStatusCode)
                {
                    string resultado = await mensaje.Content.ReadAsStringAsync();
                    temporal = JsonConvert.DeserializeObject<List<Estado>>(resultado);
                }
            }
            return temporal;
        }
        public async Task<IActionResult> registrarProducto()
        {
            string pruebap = "";
            //ISession["Agendas"] = List;
            //ViewBag.usuario = HttpContext.Session.GetString("sesion");
            //ViewBag.prueba = new LoginController().buscarvendedor(ViewBag.usuario);
            pruebap = Convert.ToString(lc.RECIBIR());
            //string mensaje = ViewBag.vendedor;
            ViewBag.tipoestado = new SelectList(await tipoestado(), "idEstado", "descripcion");
          
            return View(await Task.Run(() => new Producto()));
        }

        [HttpPost]
        public async Task<IActionResult> registrarProducto(Producto reg, IFormFile imagen1, IFormFile imagen2, IFormFile imagen3)
        {

            using (var target = new MemoryStream())
            {

                imagen1.CopyTo(target);
                //reg.imagen = target.ToArray();
                byte[] imagenByte = target.ToArray();
                reg.imagen1 = imagenByte;
            }
            using (var target = new MemoryStream())
            {

                imagen2.CopyTo(target);
                //reg.imagen = target.ToArray();
                byte[] imagenByte = target.ToArray();
                reg.imagen2 = imagenByte;
            }
            using (var target = new MemoryStream())
            {

                imagen3.CopyTo(target);
                //reg.imagen = target.ToArray();
                byte[] imagenByte = target.ToArray();
                reg.imagen3 = imagenByte;
            }
             

            string mensaje = "";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/api/Producto/");

                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(reg), System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage msg = await client.PostAsync("agregarproducto", content);
                if (msg.IsSuccessStatusCode)
                {
                    mensaje = await msg.Content.ReadAsStringAsync();
                }
            }
            ViewBag.mensaje = mensaje;
            ViewBag.tipoestado = new SelectList(await tipoestado(), "idEstado", "descripcion");

            return View(reg);
        }
    }
}
