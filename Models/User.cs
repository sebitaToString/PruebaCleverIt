using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaSRamos.Models
{
    public class User
    {
        public string nombre { get; set; }
        public string apellido { get; set; }

        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        public string profesion { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }


    }

    public class datosUser : User
    {
        public string id { get; set; }

    }
     
}
