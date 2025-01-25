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

            var resultado = tempo + potencia;
            ViewBag.Resultado = resultado;

            return View("Index");
        }

    }
}