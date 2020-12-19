using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PruebaTecnicaSRamos.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaSRamos.utilidades
{
    public class Funciones
    {
        private readonly IConfiguration conf;
        private readonly string urlUserData;
         
        public Funciones(IConfiguration configuration)
        {
            conf = configuration;
            urlUserData = conf["urlUserData"];
        }
        public  string GenerateJSONWebToken(Login userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(conf["Jwt:Issuer"],
              conf["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> autenticar(Login log)
        {

            bool token = false;
            try
            {
                using (var cliente = new HttpClient())
                {
                    using (var resp = await cliente.GetAsync(urlUserData + "?username=" + log.username + "&password=" + log.password))
                    {
                        string strResp = await resp.Content.ReadAsStringAsync();
                        List<UserData> usuarios = JsonConvert.DeserializeObject<List<UserData>>(strResp);

                        token = usuarios.Count > 0;

                    }
                }
            }
            catch (Exception e)
            {
                token = false;
            }

            return token;

        }
    }
}
