﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DireccionMapa
    {
        public int Id { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Parroquia { get; set; }
        public string Direccion { get; set; }
        public string Referencia { get; set; }
    }
}
