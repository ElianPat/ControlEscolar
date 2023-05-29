using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosLinqEscolar.Modelo
{
    public class Mensaje
    {
        public Boolean error { get; set; }

        public String mensaje { get; set; }

        public usuario usuarioLogin { get; set; }
    }
}