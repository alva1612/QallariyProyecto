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
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("https://localhost:5001/api/Negocio/");

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

            return View(temporal);
        }

    }
}
