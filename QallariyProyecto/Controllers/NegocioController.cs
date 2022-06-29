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
            ViewBag.departamentos = new SelectList(await uc.departamentos(), "idDepartamento", "descripcion");
            ViewBag.distritos = new SelectList(await uc.distritos(), "idDistrito", "descripcion");
            ViewBag.cat = new SelectList(await uc.categorias(), "idCategoria", "descripcion");
            return View(await Task.Run(() => new NegocioUpload()));
        }

        [HttpPost] public async Task<IActionResult>registrar(NegocioUpload reg, IFormFile imagen)
        {
            
            if (reg != null)
            {
                if (imagen != null)
                {

                    using (var target = new MemoryStream())
                    {
                        
                        imagen.CopyTo(target);
                        //reg.imagen = target.ToArray();
                        byte[] imagenByte =  target.ToArray();
                        reg.imagen = imagenByte;
            }
                    
                }
            }
            
            string mensaje = "";
            var userId = HttpContext.Request.Cookies["user_id"];
            reg.vendedor = Int32.Parse(userId);
            using (var client= new HttpClient())
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
            ViewBag.departamentos = new SelectList(await uc.departamentos(), "idDepartamento", "descripcion");
            ViewBag.distritos = new SelectList(await uc.distritos(), "idDistrito", "descripcion");
            ViewBag.cat = new SelectList(await uc.categorias(), "idCategoria", "descripcion");

            return RedirectToAction("registrar", "Negocio");
        }
        
    }
}
