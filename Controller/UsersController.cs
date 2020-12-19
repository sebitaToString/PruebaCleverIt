using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PruebaTecnicaSRamos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaSRamos.Controller
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        User _user = new User();

        private readonly IConfiguration conf;
        private readonly string urlUser;


        public UsersController(IConfiguration configuration)
        {
            conf = configuration;
            urlUser = conf["urlUser"];
        }



        /// <summary>
        /// lista todos los usuarios de *USER*
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("")]
        public async Task<RespuestaTodos> listarTodos()
        {
            RespuestaTodos respuesta = new RespuestaTodos();
            try
            {

                List<datosUser> _lista = new List<datosUser>();
                using (var cliente = new HttpClient())
                {
                    using (var resp = await cliente.GetAsync(urlUser))
                    {
                        string strResp = await resp.Content.ReadAsStringAsync();
                        respuesta.codigo = resp.StatusCode.ToString();

                        ICollection<object> respServ = JsonConvert.DeserializeObject<ICollection<object>>(strResp);

                        foreach (var nodo in respServ)
                        {
                            if (nodo.GetType().Name.ToLower().Equals("jarray"))
                            {
                                JsonConvert.DeserializeObject<List<datosUser>>(nodo.ToString()).ForEach(i => _lista.Add(i));
                            }
                            else
                            {
                                _lista.Add(JsonConvert.DeserializeObject<datosUser>(nodo.ToString()));
                            }
                        }
                    }
                }
                respuesta.mensaje = "Proceso completo";
                respuesta.users = _lista;
            }
            catch (Exception e)
            {
                respuesta.mensaje = e.Message;
                respuesta.codigo = "-1";
            }

            return respuesta;
        }




        /// <summary>
        /// Obtiene usuario especifico por id
        /// </summary>
        /// <param name="idUser">test</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("{idUser}")]
        public async Task<RespuestaTodos> getById(string idUser)
        {

            RespuestaTodos respuesta = new RespuestaTodos();
            try
            {

                using (var cliente = new HttpClient())
                {
                    using (var resp = await cliente.GetAsync(urlUser + "/" + idUser))
                    {
                        string strResp = await resp.Content.ReadAsStringAsync();
                        respuesta.users.Add(JsonConvert.DeserializeObject<datosUser>(strResp));
                        respuesta.codigo = resp.StatusCode.ToString();
                        respuesta.mensaje = "OK";
                    }
                }
            }
            catch (Exception e)
            {
                respuesta.mensaje = e.Message;
                respuesta.codigo = "-1";
            }


            return respuesta;
        }






        /// <summary>
        /// Actualiza el usuario segun el id proprocionado
        /// </summary>
        /// <param name="user"></param>
        /// <param name="idUser"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("{idUser}")]

        public async Task<RespuestaTodos> UpdUser([FromBody] User user, string idUser)
        {
            RespuestaTodos respuesta = new RespuestaTodos();
            try
            {
                using (var cliente = new HttpClient())
                {
                    StringContent contenido = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                    using (var resp = await cliente.PutAsync(urlUser + "/" + idUser, contenido))
                    {
                        string strResp = await resp.Content.ReadAsStringAsync();
                        respuesta.users.Add(JsonConvert.DeserializeObject<datosUser>(strResp));
                        respuesta.codigo = resp.StatusCode.ToString();
                        respuesta.mensaje = "Actualizacion completa";
                    }



                }
            }
            catch (Exception e)
            {
                respuesta.mensaje = e.Message;
                respuesta.codigo = "-1";
            }

            return respuesta;
        }



        /// <summary>
        /// Crea usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("")]

        public async Task<RespuestaTodos> AddUser([FromBody] User user)
        {
            RespuestaTodos respuesta = new RespuestaTodos();
            try
            {
                using (var cliente = new HttpClient())
                {
                    StringContent contenido = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                    using (var resp = await cliente.PostAsync(urlUser, contenido))
                    {
                        string strResp = await resp.Content.ReadAsStringAsync();
                        respuesta.users.Add(JsonConvert.DeserializeObject<datosUser>(strResp));
                        respuesta.codigo = resp.StatusCode.ToString();
                        respuesta.mensaje = "Creacion completa";
                    }

                }
            }
            catch (Exception e)
            {
                respuesta.mensaje = e.Message;
                respuesta.codigo = "-1";
            }

            return respuesta;
        }




        /// <summary>
        /// Borra el usuario 
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("{idUser}")]

        public async Task<RespuestaEliminar> Delete(string idUser)
        {
            RespuestaEliminar respuesta = new RespuestaEliminar();
            try
            {

                string mensaje = string.Empty;
                _user = new User();
                using (var cliente = new HttpClient())
                {
                    using (var resp = await cliente.DeleteAsync(urlUser + "/" + idUser))
                    {

                        mensaje = await resp.Content.ReadAsStringAsync();
                        respuesta.id = idUser;
                        respuesta.codigo = resp.StatusCode.ToString();
                        respuesta.mensaje = "Eliminacion completa";
                    }
                }
            }
            catch (Exception e)
            {
                respuesta.mensaje = e.Message;
                respuesta.codigo = "-1";
            }


            return respuesta;
        }
    }
}
