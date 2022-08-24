using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzaria_G11.Data;
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

        [HttpPost]
        public IActionResult Atualizar(int? id)
        {
            if (id == null)
                return NotFound();

            var result = _context.Sabores.FirstOrDefault(s => s.Id == id);

            if (result == null)
                return View();

            return View(result);

        }

        [HttpPost]
        public IActionResult Criar(int id)
        {
            var resultado = _context.Sabores
            .Include(prod => prod.PizzasSabores)
            .ThenInclude(t => t.Tamanho)
            .FirstOrDefault(saborid => saborid.Id == id);

            if (resultado == null)
                return View("NotFound");

            return View(resultado);
        }
     
        public IActionResult Deletar(int id)
        {
            var result = _context.Sabores.FirstOrDefault(sab => sab.Id == id);

            if (result == null) return View();

            return View(result);
        }
        [HttpPost, ActionName("Deletar")]
        public IActionResult ConfirmarDeletar(int id)
        {
            var result = _context.Sabores.FirstOrDefault(sab => sab.Id == id);
            _context.Sabores.Remove(result);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Detalhes(int id)
        {
                var resultado = _context.Sabores
                .Include(piz => piz.PizzasSabores)
                .ThenInclude(tam =>tam.Tamanho)
                .FirstOrDefault(sab => sab.Id == id);
                
                if (resultado == null)
                return View("NotFound");

                return View(resultado);
        }

    }
}


