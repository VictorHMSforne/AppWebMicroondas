﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMicroondas.Models
{
    // Classe Microondas com suas propriedades
    public class Microondas
    {
        public int? Tempo { get; set; }
        public int? Potencia { get; set; }
        public string Mensagem { get; set; }
    }
}