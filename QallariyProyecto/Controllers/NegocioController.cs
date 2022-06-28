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
    public class NegocioController : Controller
    {
        UtilsController uc = new UtilsController();
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> registrar()
        {
            //ViewBag.departamentos = new SelectList(await uc.departamentos(), "idDepartamento", "descripcion");
            

            ViewBag.Departamentos = new SelectList(await uc.departamentos(), "idDepartamento", "descripcion");
            ViewBag.Provincias = new SelectList(await uc.provincias(0), "idProvincia", "descripcion");
            ViewBag.Distritos = new SelectList(await uc.distritos(0), "idDistrito", "descripcion");

            return View(await Task.Run(() => new NegocioUpload()));

        }

        [HttpPost] public async Task<IActionResult>registrar(NegocioUpload reg, IFormFile imagen)
        {

            ViewBag.Departamentos = new SelectList(await uc.departamentos(), "idDepartamento", "descripcion");
            ViewBag.Provincias = new SelectList(await uc.provincias(0), "idProvincia", "descripcion");
            ViewBag.Distritos = new SelectList(await uc.distritos(0), "idDistrito", "descripcion");

            /*
            if (reg.imagen != null)
            {
                if (reg.imagen.Length>0)
                {*/

            using (var target = new MemoryStream())
                    {
                        
                        imagen.CopyTo(target);
                        //reg.imagen = target.ToArray();
                        byte[] imagenByte =  target.ToArray();
                        reg.imagen = imagenByte;
            }/*      
                    
                }
            }*/
            
            string mensaje = "";

            using(var client= new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44375/api/Negocio/");

                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(reg), System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage msg = await client.PostAsync("agregarnegocio", content);
                if (msg.IsSuccessStatusCode)
                {
                    mensaje = await msg.Content.ReadAsStringAsync();
                }
            }
            ViewBag.mensaje = mensaje;
            ViewBag.departamentos = new SelectList(await uc.departamentos(), "idDepartamento", "descripcion");

            return View(reg);
        }

        






    }
}
