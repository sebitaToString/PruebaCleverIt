using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PruebaTecnicaSRamos.Models;
using PruebaTecnicaSRamos.utilidades;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaSRamos.Controller
{
    [Route("Login")]
    [ApiController]
    public class LoginController : Microsoft.AspNetCore.Mvc.Controller
    {

        private readonly IConfiguration conf; 

        public LoginController(IConfiguration configuration)
        {
            conf = configuration; 
        }

        /// <summary>
        /// Api de login genera el token para la autenticacion y uso del middleware
        /// </summary>
        /// <param name="login"></param>
        /// <returns>JSON TOKEN</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            IActionResult response = Unauthorized();
            Funciones ff = new Funciones(conf);
            if (await ff.autenticar(login))
            {
                var tokenString = ff.GenerateJSONWebToken(login);
                response = Ok(new { token = tokenString });
            }

            return response;
        }
        
    }
}
