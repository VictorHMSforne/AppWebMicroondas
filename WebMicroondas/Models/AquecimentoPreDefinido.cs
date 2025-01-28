using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMicroondas.Models
{
    public class AquecimentoPreDefinido
    {
        public string Nome { get; set; }
        public string Alimento { get; set; }
        public int? Tempo { get; set; }
        public int? Potencia { get; set; }
        public string MensagemDeAquecimento { get; set; }
        public string InstrucoesComplementares { get; set; }
    }
}