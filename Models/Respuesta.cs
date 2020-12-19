using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaSRamos.Models
{
    public class Respuesta
    {

        public string mensaje { get; set; } 
        public string codigo{ get; set; }
        public Respuesta() {
            mensaje = string.Empty;
            codigo = "0"; 
        }
    }

    public class RespuestaTodos : Respuesta {
        public List<datosUser> users { get; set; }
        public RespuestaTodos()
        {
            users = new List<datosUser>();
        }
    }
 


    public class RespuestaEliminar : Respuesta
    {
        public string id { get; set; }
        public RespuestaEliminar()
        {
            id = string.Empty;
        }
    }



}
