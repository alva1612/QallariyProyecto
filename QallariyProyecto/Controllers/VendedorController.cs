using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Mvc.Rendering; //SelectList
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QallariyProyecto.Models;
using System.Net.Http;

namespace QallariyProyecto.Controllers
{
    public class VendedorController : Controller
    {
        async Task<IEnumerable<TipoDocumento>> tipoDocumento()
        {
            List<TipoDocumento> temporal = new List<TipoDocumento>();
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("https://localhost:5001/api/Utils/");
                //recibir mensaje
                HttpResponseMessage mensaje = await cliente.GetAsync("tipoDocumento");
                if (mensaje.IsSuccessStatusCode)
                {
                    string resultado = await mensaje.Content.ReadAsStringAsync();
                    temporal = JsonConvert.DeserializeObject<List<TipoDocumento>>(resultado);
                }
            }
            return temporal;
        }
        public async Task<IActionResult> registro()
        {
            
            ViewBag.tipoDocumento = new SelectList(await tipoDocumento(), "idTipDoc", "descripcion");

            return View(await Task.Run(() => new Vendedor()));
        }

        [HttpPost]
        public async Task<IActionResult> registro(Vendedor reg)
        {
            string mensaje = "";
          

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("https://localhost:5001/api/Vendedor/");

   
                StringContent content = new StringContent(
                JsonConvert.SerializeObject(reg), System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage msg = await cliente.PostAsync("agregarvendedor", content);
                if (msg.IsSuccessStatusCode)
                {

                    mensaje = await msg.Content.ReadAsStringAsync();
                }
            }

            ViewBag.mensaje = mensaje;
            ViewBag.tipoDocumento = new SelectList(await tipoDocumento(), "idTipDoc", "descripcion");

            return View(reg);
        }

        public async Task<Vendedor> Buscar(string correo)
        {
            Vendedor temporal = new Vendedor();
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("https://localhost:5001/api/Vendedor/");
                //recibir mensaje
                HttpResponseMessage mensaje = await cliente.GetAsync($"buscar?correo={correo}");
                if (mensaje.IsSuccessStatusCode)//200
                {
                    string resultado = await mensaje.Content.ReadAsStringAsync();
                    temporal = JsonConvert.DeserializeObject<Vendedor>(resultado);
                }
            }
            return temporal;
        }

        public async Task<IActionResult> EditarVendedor(string correo)
        {

            Vendedor reg = await Buscar(correo);
            ViewBag.tipoDocumento = new SelectList(await tipoDocumento(), "idTipDoc", "descripcion");
 
            return View(await Task.Run(() => reg));
        }
        [HttpPost]
        public async Task<IActionResult> EditarVendedor(Vendedor reg)
        {
            string mensaje = "";
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("https://localhost:5001/api/Vendedor/");

                StringContent content = new StringContent(
                JsonConvert.SerializeObject(reg), System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage msg = await cliente.PutAsync("actualizarvendedor", content);
                if (msg.IsSuccessStatusCode)
                {
                    mensaje = await msg.Content.ReadAsStringAsync();
                }
            }



            ViewBag.mensaje = mensaje;
            ViewBag.tipoDocumento = new SelectList(await tipoDocumento(), "idTipDoc", "descripcion");

            return View(reg);
        }

    }
}
