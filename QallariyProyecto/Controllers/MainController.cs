using Microsoft.AspNetCore.Mvc;
using QallariyProyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QallariyProyecto.Controllers
{
    public class MainController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Negocio> temporal = new List<Negocio>();
            List<Producto> productos = new List<Producto>();
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("https://localhost:44375/api/Negocio/");

                HttpResponseMessage mensaje = await cliente.GetAsync("listanegocio");
                if (mensaje.IsSuccessStatusCode)
                {
                    string resultado = await mensaje.Content.ReadAsStringAsync();
                    temporal = JsonConvert.DeserializeObject<List<Negocio>>(resultado);

                    temporal.ForEach((Negocio item) =>
                    {
                        item.hasImage = false;
                        if (item.imagen != null)
                            item.hasImage = true;
                    });
                }
            }

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("https://localhost:44375/api/Producto/");

                HttpResponseMessage mensaje = await cliente.GetAsync("listaproducto");
                if (mensaje.IsSuccessStatusCode)
                {
                    string resultado = await mensaje.Content.ReadAsStringAsync();
                    productos = JsonConvert.DeserializeObject<List<Producto>>(resultado);

                }
            }
            ViewBag.productos = productos;
            return View(temporal);
        }
        [HttpPost]
        public async Task<IActionResult> VerNegocio(string idNegocio )
        {
            NegocioListado temporal = new NegocioListado();
            List<Producto> productos = new List<Producto>();
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("https://localhost:44375/api/Negocio/");

                HttpResponseMessage mensaje = await cliente.GetAsync($"listanegocioxid?id={idNegocio}");
                if (mensaje.IsSuccessStatusCode)
                {
                    string resultado = await mensaje.Content.ReadAsStringAsync();
                    temporal = JsonConvert.DeserializeObject<NegocioListado>(resultado);
                }
            }
            using (var cliente2 = new HttpClient())
            {
                cliente2.BaseAddress = new Uri("https://localhost:44375/api/Producto/");

                HttpResponseMessage mensaje = await cliente2.GetAsync($"listaproductoxidnegocio?id={idNegocio}");
                if (mensaje.IsSuccessStatusCode)
                {
                    string resultado2 = await mensaje.Content.ReadAsStringAsync();
                    productos = JsonConvert.DeserializeObject<List<Producto>>(resultado2);
                    foreach (Producto p in productos) {
            
                    }
                }
            }
            ViewBag.productos = productos;

            return View(temporal);
        }

    }
}
