using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzaria.Models.ViewModels.ResponseDTO;
using Pizzaria_G11.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzaria.Controllers
{
    public class PizzaController : Controller
    {
        private PizzariaDbContext _context;

        public PizzaController(PizzariaDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Pizzas);
        }

        [HttpPost]
        public IActionResult Atualizar(int? id)
        {
            if (id == null)
                return NotFound();

            var result = _context.Pizzas.FirstOrDefault(p => p.Id == id);

            if (result == null)
                return View();

            return View(result);

        }
        [HttpPost]
        public IActionResult Criar(int id)
        {
                var resultado = _context.Pizzas
                .Include(prod => prod.PizzasSabores)
                .ThenInclude(t => t.Tamanho)
                .FirstOrDefault(pizzaid => pizzaid.Id == id);

                if (resultado == null)
                return View("NotFound");

            return View(resultado);
        }
       
        public IActionResult Deletar(int id)
        {
            var result = _context.Pizzas.FirstOrDefault(p => p.Id == id);

            if (result == null) return View();

            return View(result);
        }
        [HttpPost, ActionName("Deletar")]
        public IActionResult ConfirmarDeletar(int id)
        {
            var result = _context.Pizzas.FirstOrDefault(p => p.Id == id);
            _context.Pizzas.Remove(result);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Detalhes(int id)
        {
               var result = _context.Pizzas.Where(prod => prod.Id == id)
               .Select(prod => new GetPizzasDTO()
               {
                   Nome = prod.Nome,
                   Descricao = prod.Descricao,
                   ImagemURL = prod.ImagemURL,
                   Preco = prod.Preco
               }).FirstOrDefault();

            return View(result);
        }
    
    }
}
