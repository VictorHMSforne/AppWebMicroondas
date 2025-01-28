using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMicroondas.Context;
using WebMicroondas.Models;

namespace WebMicroondas.Controllers
{
    public class AquecimentoController : Controller
    {

        private readonly AquecimentoContext _context = new AquecimentoContext();

        // GET: Aquecimento
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Aquecimento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AquecimentoPreDB aquecimento)
        {
            try
            {
                if (_context.AquecimentosPreDefinidos.Any(a => a.MensagemDeAquecimento == aquecimento.MensagemDeAquecimento))
                {
                    // Se já existir, adiciona um erro ao ModelState
                    ModelState.AddModelError("MensagemDeAquecimento", "A Mensagem de Aquecimento já foi cadastrada.");
                }
                if (ModelState.IsValid)
                {
                    _context.AquecimentosPreDefinidos.Add(aquecimento);
                    _context.SaveChanges();
                    return RedirectToAction("Index","Microondas");
                }
                return View();

            }
            catch
            {
                return View();
            }
        }

        // GET: Aquecimento/Edit/5
        public ActionResult Edit(int id)
        {
            // Busca o AquecimentoPreDefinido no banco de dados usando o id
            var aquecimento = _context.AquecimentosPreDefinidos.FirstOrDefault(a => a.Id == id);

            // Se não encontrar o item, retorna um erro 404 (não encontrado)
            if (aquecimento == null)
            {
                return HttpNotFound();
            }

            // Retorna a view com o item para editar
            return View(aquecimento);
        }

        // POST: Aquecimento/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, AquecimentoPreDB aquecimento)
        {
            try
            {
                // Verifica se o id do aquecimento enviado corresponde ao que queremos editar
                if (id != aquecimento.Id)
                {
                    return HttpNotFound(); // Se os IDs não baterem, retorna um erro 404
                }

                // Verifica se a MensagemDeAquecimento já existe no banco
                if (_context.AquecimentosPreDefinidos.Any(a => a.MensagemDeAquecimento == aquecimento.MensagemDeAquecimento && a.Id != id))
                {
                    // Se já existir, adiciona um erro ao ModelState
                    ModelState.AddModelError("MensagemDeAquecimento", "A Mensagem de Aquecimento já foi cadastrada.");
                }

                // Se o ModelState for válido, realiza a atualização
                if (ModelState.IsValid)
                {
                    // Atualiza o item no banco de dados
                    _context.Entry(aquecimento).State = EntityState.Modified;
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Microondas"); // Redireciona para a página de listagem
                }

                // Se o ModelState não for válido, retorna a view de edição com os erros
                return View(aquecimento);
            }
            catch
            {
                return View(); // Retorna a view caso algo dê errado
            }
        }

        // GET: Aquecimento/Delete/5

        public ActionResult Delete(int id)
        {
            var aquecimento = _context.AquecimentosPreDefinidos.FirstOrDefault(a => a.Id == id);

            // Se não encontrar o item, retorna um erro 404 (não encontrado)
            if (aquecimento == null)
            {
                return HttpNotFound();
            }

            // Retorna a view de confirmação de deleção
            return View(aquecimento);
        }

        // POST: Aquecimento/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult Delete(int id, AquecimentoPreDB aquecimento)
        {
            try
            {
                aquecimento = _context.AquecimentosPreDefinidos.FirstOrDefault(a => a.Id == id);

                if (aquecimento == null)
                {
                    return HttpNotFound(); // Se não encontrar o item, retorna um erro 404
                }

                // Remove o item do banco de dados
                _context.AquecimentosPreDefinidos.Remove(aquecimento);
                _context.SaveChanges(); // Salva as alterações no banco

                // Redireciona para a página de listagem após excluir
                return RedirectToAction("Index", "Microondas");
            }
            catch
            {
                return View(); // Se ocorrer um erro, retorna para a view
            }
        }
    }
}
