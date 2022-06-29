using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Session;
using Microsoft.Data.SqlClient;
using System.Data;
using QallariyProyecto.Models;
using Microsoft.AspNetCore.Http;

namespace QallariyProyecto.Controllers
{
    public class LoginController : Controller
    {
        string cadena = @"server=.\SQLEXPRESS;database=qallariy;Trusted_Connection=True;" +
                "MultipleActiveResultSets=True;TrustServerCertificate=False;Encrypt=False";
        string sesion = "";
        VendedorController vc = new VendedorController();
        string verifica(string corre, string clave)
        { 
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(cadena))

            {

                try

                {

                    SqlCommand cmd = new SqlCommand("usp_acceso_login", cn);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@correo", corre);

                    cmd.Parameters.AddWithValue("@clave", clave);

                    cn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (!dr.HasRows)

                        mensaje = "Datos incorrectos";

                    else

                        mensaje = "Ok";

                }

                catch (Exception ex) { mensaje = ex.Message; }

                finally { cn.Close(); }

            }

            return mensaje;

        }

        public async Task<IActionResult> Logeo()

        {

            return View(await Task.Run(() => new Vendedor()));

        }

        [HttpPost]
        public async Task<IActionResult> Logeo(Vendedor reg)

        {

            if (!ModelState.IsValid)

                return View(await Task.Run(() => reg));

            string mensaje = verifica(reg.correo, reg.password);
            


            if (mensaje != "Ok")
            {
                HttpContext.Session.SetString(sesion, "");
                ModelState.AddModelError("", mensaje);
                return View(await Task.Run(() => reg));

            }

            Vendedor v = await vc.Buscar(reg.correo);
            ModelState.AddModelError("", "");
          
            HttpContext.Session.SetString("_correo", reg.correo);
            HttpContext.Session.SetString("_id", v.idVendedor.ToString());

            return RedirectToAction("registrar", "Negocio");
        }

        public IActionResult detalleNegocio()

        {

            //almaceno en un ViewBag el contenido del session de key sesion

            ViewBag.usuario = HttpContext.Session.GetString(sesion);

            return View();

        }

        //public IActionResult detalleNegocio()
        //{
        //    return View();
        //}
    }
}
