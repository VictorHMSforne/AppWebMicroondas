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

        
        [HttpPost]
        public ActionResult IniciarAquecimento(Microondas microondas)
        {
            var tempo = microondas.Tempo;
            var potencia = microondas.Potencia;

            if (tempo < 1 || tempo > 120)
                ModelState.AddModelError("", "O Tempo deve estar entre 1 e 120 segundos");
            if (potencia < 1 || potencia > 10)
                ModelState.AddModelError("", "A Potência deve estar entre 1 e 10");

            if (!ModelState.IsValid)       
                return View("Index", microondas);

            var resultado = tempo + potencia;
            ViewBag.Resultado = resultado;
            ViewBag.PotenciaPadrao = potencia;

            return View("Index");
        }

    }
}