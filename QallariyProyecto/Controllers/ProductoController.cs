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
namespace QallariyProyecto.Controllers
{
    public class ProductoController : Controller
    {
        UtilsController uc = new UtilsController();

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> VerProducto(string idProducto)
        {
            Producto temporal = new Producto();
            List<Producto> productos = new List<Producto>();
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("https://localhost:44375/api/Producto/");

                HttpResponseMessage mensaje = await cliente.GetAsync($"listaproductoxid?id={idProducto}");
                if (mensaje.IsSuccessStatusCode)
                {
                    string resultado = await mensaje.Content.ReadAsStringAsync();
                    temporal = JsonConvert.DeserializeObject<Producto>(resultado);
                }
            }

            return View(temporal);
        }

        public async Task<IActionResult> registrar()
        {
            //ViewBag.departamentos = new SelectList(await uc.departamentos(), "idDepartamento", "descripcion");
      
            return View(await Task.Run(() => new Producto()));

        }

        [HttpPost]
        public async Task<IActionResult> registrar(Producto reg, IFormFile imagen)
        {


            using (var target = new MemoryStream())
            {

                imagen.CopyTo(target);
  
                byte[] imagenByte = target.ToArray();
                reg.imagen1 = imagenByte;
                reg.imagen2 = imagenByte;
                reg.imagen3 = imagenByte;
            }



            string mensaje = "";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44375/api/Producto/");

                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(reg), System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage msg = await client.PostAsync("agregarproducto", content);
                if (msg.IsSuccessStatusCode)
                {
                    mensaje = await msg.Content.ReadAsStringAsync();
                }
            }
            ViewBag.mensaje = mensaje;
           
            return View(reg);
        }


    }
}
