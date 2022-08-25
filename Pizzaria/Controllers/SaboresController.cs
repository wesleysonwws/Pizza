using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzaria.Models;
using Pizzaria_G11.Data;
using PizzariaAtv.Models.ViewModels.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzaria.Controllers
{
    public class SaboresController : Controller
    {
        private PizzariaDbContext _context;

        public SaboresController(PizzariaDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Sabores);
        }

        public IActionResult Detalhes(int id)
        {
            var resultado = _context.Sabores
                .Include(af => af.PizzasSabores)
                .ThenInclude(f => f.Pizza)
                .FirstOrDefault(sabor => sabor.Id == id);

            if (resultado == null)
                return View("NotFound");

            return View(resultado);
        }

        public IActionResult Criar() => View();

        [HttpPost]
        public IActionResult Criar(PostSaborDTO saborDTO)
        {
            if (!ModelState.IsValid)
                return View(saborDTO);

            Sabor sabor = new Sabor(saborDTO.Nome, saborDTO.ImagemURL);
            _context.Sabores.Add(sabor);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Atualizar(int? id)
        {
            if (id == null)
                return NotFound();

            var result = _context.Sabores.FirstOrDefault(a => a.Id == id);

            if (result == null)
                return View();

            return View(result);
        }

        [HttpPost]
        public IActionResult Atualizar(int id, PostSaborDTO saborDTO)
        {
            var sabor = _context.Sabores.FirstOrDefault(a => a.Id == id);

            if (!ModelState.IsValid)
                return View(sabor);

            sabor.AtualizarDados(saborDTO.Nome, saborDTO.ImagemURL);

            _context.Update(sabor);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Deletar(int id)
        {
            var result = _context.Sabores.FirstOrDefault(a => a.Id == id);

            if (result == null) return View();

            return View(result);
        }

        [HttpPost, ActionName("Deletar")]
        public IActionResult ConfirmarDeletar(int id)
        {
            var result = _context.Sabores.FirstOrDefault(a => a.Id == id);
            _context.Sabores.Remove(result);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}