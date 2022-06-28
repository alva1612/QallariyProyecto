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
        string prueba = "";

        string cadena = @"server= DESKTOP-SHU1TP6; database=qallariy; Trusted_Connection=true; " +
           "MultipleActiveResultSets=true; TrustServerCertificate=False; Encrypt=False ";
        //string sesion = "";

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
        public Vendedor buscarvendedor(string correo)
        {
            Vendedor vendedor = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(cadena))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand("SP_BUSCARXCORREO", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CORREO", correo);

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        vendedor = new Vendedor()
                        {
                            idVendedor = dr.GetInt32(0),
                            nombres = dr.GetString(1),
                            apellidos = dr.GetString(2),
                            correo = dr.GetString(3),
                            password = dr.GetString(4),
                            idTipDoc = dr.GetInt32(5),
                            numeroDocumento = dr.GetInt32(6),
                            fechaNacimiento = dr.GetDateTime(7),
                            telefono = dr.GetString(8)
                        };
                    }

                    dr.Close();
                    cn.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return vendedor;
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

                HttpContext.Session.SetString("", "");


                ModelState.AddModelError("", mensaje);



                return View(await Task.Run(() => reg));

            }


            ModelState.AddModelError("", "");
           
                HttpContext.Session.SetString("_correo", reg.correo);
            
            

           

            return RedirectToAction("detalleNegocio");



        }

        //new buscarvendedor();

        //var vendedorprueba = new buscarvendedor(ViewBag.usuario);

        public IActionResult detalleNegocio()

        {

            //almaceno en un ViewBag el contenido del session de key sesion
            String carlo = HttpContext.Session.GetString("_correo");
            ViewBag.usuario = carlo;
            String prueba = carlo;
            HttpContext.Session.SetString("_correo", carlo);
            ViewBag.vendedor = buscarvendedor(prueba);
            //TempData["ViewBag.vendedor"] = data;

            return View();
        }

        ////public ActionResult pruebap()
        //{
           
            
        //    return RedirectToAction("registrarProducto","Producto");
        //}

        public Vendedor RECIBIR()
        {

          var adrian = HttpContext.Session.GetString("_correo");
            if (adrian == null || adrian == "")
            {

            }
          ViewBag.PRUEBAP = adrian;
            //dynamic prueba = ViewBag.usuario;
            ViewBag.vendedor = buscarvendedor(ViewBag.PRUEBAP);

            Vendedor pruebe = ViewBag.vendedor;

            return pruebe;
        }


        //var vendedorprueba = new buscarvendedor(prueba);

        //public IActionResult detalleNegocio()
        //{
        //    return View();
        //}


    }
}
