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
        
        public ActionResult Index()
        {
            return View();
        }

        // Método para iniciar o aquecimento
        [HttpPost]
        public ActionResult IniciarAquecimento(Microondas microondas)
        {
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
            // Se não for atendido, recebe o que o usuário inseriu
            else
            {
                ViewBag.TempoInput = tempo;
                ViewBag.Potencia = potencia ?? 10;
            }
            // Verificação para saber se o aquecimento foi iniciado
            if (mensagem != null && mensagem.Contains("Aquecimento Iniciado"))
            {
                tempo += 30;
                ViewBag.TempoJS = tempo;
                ViewBag.TempoInput = tempo;
                return View("Index");
            }

            // Atualiza o tempo no frontend
            ViewBag.TempoJS = tempo;
            ViewBag.Potencia = potencia ?? 10;

            return View("Index");
        }
    }
}