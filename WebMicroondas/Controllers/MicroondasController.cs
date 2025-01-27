using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMicroondas.Models;

namespace WebMicroondas.Controllers
{
    public class MicroondasController : Controller
    {
        // Variáveis globais para poder auxiliar na validação e na troca de dados de uma view para outra
        public static string AquecimentoParado = "";
        public static string MensagemDoMicroondas;
        public static bool SegundaVezNaPaginaCancelar = false;

        public ActionResult Index()
        {
            return View();
        }

        // Método para iniciar o aquecimento
        [HttpPost]
        public ActionResult IniciarAquecimento(Microondas microondas)
        {
            // Essa ViewBag é usada posteriormente, com o objetivo de ajudar nas validações dos if's
            ViewBag.AquecimentoParado = AquecimentoParado;
            
            // Recebe as variáveis que o usuário inseriu
            int? tempo = microondas.Tempo;
            int? potencia = microondas.Potencia;
            TimeSpan tempoEmMinutos = new TimeSpan();
            string exibirNoInput;
            string mensagem = microondas.Mensagem;

            // Caso nenhum valor seja passado, Começa o Tempo em 30s e a potência em 10
            if (tempo == null && potencia == null)
            {
                ViewBag.TempoInput = 30;
                ViewBag.TempoJS = 30;
                ViewBag.Potencia = 10;
                ModelState.Clear();
                return View("Index");
            }

            // Validações para tempo e potência
            if (tempo == null || tempo < 1 || tempo > 120)
                ModelState.AddModelError("", "O Tempo deve estar entre 1 e 120 segundos");
            if (potencia == null)
                ViewBag.Potencia = 10; // Potência padrão, caso não seja informada
            if (potencia < 1 || potencia > 10)
                ModelState.AddModelError("", "A Potência deve estar entre 1 e 10");
            if (!ModelState.IsValid)
                return View("Index", microondas);

            // Caso o usuário insira um tempo maior que 60 e menor que 100, irá exibir no formato mm:ss
            if (tempo > 60 && tempo < 100)
            {
                tempoEmMinutos = TimeSpan.FromSeconds(tempo ?? 0);
                exibirNoInput = tempoEmMinutos.ToString(@"mm\:ss");
                ViewBag.TempoInput = exibirNoInput;
                ViewBag.Potencia = potencia ?? 10;
            }
            // Se não for atendido, recebe o que o usuário inseriu de tempo e potência
            else
            {
                ViewBag.TempoInput = tempo;
                ViewBag.Potencia = potencia ?? 10;
            }
            /* Verifica se os inputs disfarçados possuem algum dado de outra operação logo após o 1° Cancelamento.
               Caso possuir algum dado dentro desses inputs, ele irá pegar o necessário e limpar a variável de apoio que é atribuída no Método CancelarAquecimento()*/
            if ((mensagem != null && mensagem.Contains("Aquecimento Concluído!") && AquecimentoParado.Contains("Aquecimento Parado!") || (mensagem != null && mensagem.Contains("Aquecimento Concluído!") && String.IsNullOrEmpty(AquecimentoParado)) ))
            {
                ViewBag.MensagemDoMicroondas = "Aquecimento Iniciado ";
                ViewBag.TempoJS = tempo;
                ViewBag.TempoInput = tempo;
                SegundaVezNaPaginaCancelar = false; // Variável Global onde é atribuida a 1° vez no CancelarAquecimento()
                return View("Index");
            }
            // Faz a verificação se o Aquecimento estava parado. Caso estiver parado, recebe os dados do método CancelarAquecimento() e passa os dados para a Index(), continuando o aquecimento de onde parou.
            else if (AquecimentoParado != null && AquecimentoParado.Contains("Aquecimento Parado!"))
            {
                ViewBag.MensagemDoMicroondas = MensagemDoMicroondas;
                ViewBag.TempoJS = tempo;
                ViewBag.TempoInput = tempo;
                SegundaVezNaPaginaCancelar = false;
                return View("Index");
            }
            // Se o usuário não inserir nenhum valor, irá preencher automaticamente conforme pedido
            else if (mensagem != null && mensagem.Contains("Aquecimento Iniciado"))
            {
                tempo += 30;
                ViewBag.MensagemDoMicroondas = "Aquecimento Iniciado ";
                ViewBag.TempoJS = tempo;
                ViewBag.TempoInput = tempo;
                return View("Index");
            }

            // Caso nenhum dos if's sejam atentidos ele retorna os dados para a View novamente
            ViewBag.TempoJS = tempo;
            ViewBag.Potencia = potencia ?? 10;

            return View("Index");
        }

        // Cópia da Página Index, alterando somente o nome
        public ActionResult CancelarAquecimento()
        {
            return View();
        }

        // Método Post para Cancelar o Aquecimento
        [HttpPost]
        public ActionResult CancelarAquecimento(Microondas microondas)
        {
            // Recebendo os dados do usuário pelo forms
            int? tempo = microondas.Tempo;
            int? potencia = microondas.Potencia;
            string mensagem = microondas.Mensagem;

            // Caso o usuário clique em cancelar pela 2ª vez, reseta todos os campos
            if ((tempo != null && potencia != null && mensagem == null) || (tempo == null && potencia == null && mensagem == null))
            {
                AquecimentoParado = "";
                MensagemDoMicroondas = null;
                ViewBag.TempoInput = null;
                ViewBag.Potencia = null;
                ViewBag.AquecimentoParado = null;
                return View();
            }
            // Verifica se o usuário clicou novamente no botão cancelar, após ter começado outro aquecimento. Se tiver clicado ele limpa os dados anteriores e recebe os novos para a view Index()
            else if (SegundaVezNaPaginaCancelar == true)
            {
                ViewBag.TempoInput = tempo;
                ViewBag.Potencia = potencia;


                MensagemDoMicroondas = mensagem; // Mensagem Indicando onde parou o aquecimento
                AquecimentoParado = "Aquecimento Parado!"; // Mensagem para ser enviada na view Atual e atribuindo o valor em uma Variável global(Lá no ínicio);
                ViewBag.AquecimentoParado = AquecimentoParado;
                return View("Index");
            }

            // Passa os dados para a a view de CancelarAquecimento(), que estão vindo da Index()
            ViewBag.TempoInput = tempo;
            ViewBag.Potencia = potencia;


            MensagemDoMicroondas = mensagem; // Mensagem Indicando onde parou o aquecimento
            AquecimentoParado = "Aquecimento Parado!"; // Mensagem para ser enviada na view Atual e atribuindo o valor em uma Variável global(Lá no ínicio);
            ViewBag.AquecimentoParado = AquecimentoParado;
            SegundaVezNaPaginaCancelar = true; // Dizendo que essa página já foi acessada uma 1° vez




            // Retorna para uma nova página com as informações
            return View(); 
        }
    }

}
