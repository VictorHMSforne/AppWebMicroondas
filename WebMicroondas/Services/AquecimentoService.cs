using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMicroondas.Models;

namespace WebMicroondas.Services
{
    public class AquecimentoService
    {
        public List<AquecimentoPreDefinido> ObterAquecimentosPreDefinidos()
        {
            return new List<AquecimentoPreDefinido>
        {
            new AquecimentoPreDefinido
            {
                Nome = "Pipoca",
                Alimento = "Pipoca(de micro-ondas)",
                Tempo = 180,
                Potencia = 7,
                MensagemDeAquecimento = "*",
                InstrucoesComplementares = "Observar o barulho de estouros do milho, caso houver um " +
                                           "intervalo de mais de 10 segundos entre um estouro e outro, interrompa o aquecimento."
            },
            new AquecimentoPreDefinido
            {
                Nome = "Leite",
                Alimento = "Leite",
                Tempo = 300,
                Potencia = 5,
                MensagemDeAquecimento = "-",
                InstrucoesComplementares = "Cuidado com aquecimento de líquidos, o choque térmico aliado ao movimento do " +
                                           "recipiente pode causar fervura imediata causando risco de queimaduras."
            },
            new AquecimentoPreDefinido
            {
                Nome = "Carnes de boi",
                Alimento = "Carne em pedaço ou fatias",
                Tempo = 840,
                Potencia = 4,
                MensagemDeAquecimento = "[",
                InstrucoesComplementares = "Interrompa o processo na metade e vire o conteúdo com " +
                                           "a parte de baixo para cima para o descongelamento uniforme."
            },
            new AquecimentoPreDefinido
            {
                Nome = "Frango",
                Alimento = "Frango (qualquer corte)",
                Tempo = 480,
                Potencia = 7,
                MensagemDeAquecimento = "]",
                InstrucoesComplementares = "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para " +
                                           "cima para o descongelamento uniforme."
            },
            new AquecimentoPreDefinido
            {
                Nome = "Feijão",
                Alimento = "Feijão congelado",
                Tempo = 480,
                Potencia = 9,
                MensagemDeAquecimento = "+",
                InstrucoesComplementares = "Deixe o recipiente destampado e em casos de plástico, cuidado ao retirar o recipiente " +
                                           "pois o mesmo pode perder resistência em altas temperaturas."
            }
        };
        }
    }
}