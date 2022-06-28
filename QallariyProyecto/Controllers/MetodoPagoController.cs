using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QallariyProyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace QallariyProyecto.Controllers
{
    public class MetodoPagoController : Controller
    {
        VendedorController vc = new VendedorController();
        public async Task<IActionResult> metodosPago()
        {
            //IHttpContextAccessor httpcontext;
            String id = HttpContext.Session.GetString("_id");
            //NOOOOOOOOO JALAAAAAAAAAAA EL ID AAAAAA
            List<MetodoPago> temporal = new List<MetodoPago>();


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44375/api/MetodoPago/");

                HttpResponseMessage mensaje = await client.GetAsync("listarmetodopagovendedor?metodoPago="+id);
                if (mensaje.IsSuccessStatusCode)
                {
                    string resultado = await mensaje.Content.ReadAsStringAsync();
                    temporal = JsonConvert.DeserializeObject<List<MetodoPago>>(resultado);
                }

                return View(temporal);
            }
        }
        public async Task<IActionResult> eliminarMetodo(int id)
        {
            string mensaje = "";
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("https://localhost:44375/api/MetodoPago/");

                HttpResponseMessage msg = await cliente.DeleteAsync("eliminarmetodo?id=" + id);
                if (msg.IsSuccessStatusCode)
                {
                    mensaje = await msg.Content.ReadAsStringAsync();
                }
            }

            return await metodosPago();
        }
    } }


