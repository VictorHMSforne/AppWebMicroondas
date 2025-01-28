using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMicroondas.Models;

namespace WebMicroondas.ViewsModels
{
    public class MicroondasViewModel
    {
        public IEnumerable<AquecimentoPreDefinido> AquecimentoPreDefinidos { get; set; }
        public Microondas Microondas { get; set; }

        // Método estático para simplificar a criação do ViewModel
        public static MicroondasViewModel CriarViewModel(Microondas microondas, IEnumerable<AquecimentoPreDefinido> aquecimentos)
        {
            return new MicroondasViewModel
            {
                Microondas = microondas,
                AquecimentoPreDefinidos = aquecimentos
            };
        }
    }
}