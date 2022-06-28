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
    public class UtilsController : Controller
    {
        public async Task<IEnumerable<Departamento>> departamentos()
        {
            List<Departamento> temporal = new List<Departamento>();
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("https://localhost:5001/api/Utils/");
                //recibir mensaje
                HttpResponseMessage mensaje = await cliente.GetAsync("departamentos");
                if (mensaje.IsSuccessStatusCode)//200
                {
                    string resultado = await mensaje.Content.ReadAsStringAsync();
                    temporal = JsonConvert.DeserializeObject<List<Departamento>>(resultado);
                }
            }
            return temporal;
        }

        public async Task<IEnumerable<Provincia>> provincias(int id)
        {
            List<Provincia> temporal = new List<Provincia>();
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("https://localhost:5001/api/Utils/");
                //recibir mensaje
                HttpResponseMessage mensaje = await cliente.GetAsync("provincia?id="+ id);
                if (mensaje.IsSuccessStatusCode)//200
                {
                    string resultado = await mensaje.Content.ReadAsStringAsync();
                    temporal = JsonConvert.DeserializeObject<List<Provincia>>(resultado);
                }
            }
            return temporal;
        }

        //async Task<IEnumerable>
    }
}
