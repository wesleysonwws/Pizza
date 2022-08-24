using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pizzaria.Models;
using Pizzaria.Models.ViewModels.ResponseDTO;
using Pizzaria_G11.Data;
using PizzariaAtv.Models.ViewModels.Request;
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
            var res = _context.Pizzas;
            return View(res);
        }

        [HttpPost]
        public IActionResult Atualizar(int id)
        {
            var result = _context.Pizzas.Include(x => x.PizzasSabores).ThenInclude(x => x.SaborId)
                                        .FirstOrDefault(x => x.Id == id);
            if (result == null) return View("NotFound");
            var resp = new PostPizzaDTO()
            {
                Nome = result.Nome,
                Descricao = result.Descricao,
                Preco = result.Preco,
                ImagemURL = result.ImagemURL,
                SaboresId = result.PizzasSabores.Select(x => x.SaborId).ToList()
            };
            DadosDropDow();
            return View(resp);
        }
        [HttpPost]
        public IActionResult Atualizar(int id, PostPizzaDTO pizzaDTO)
        {
            var result = _context.Pizzas.FirstOrDefault(x => x.Id == id);
            if (!ModelState.IsValid) return View(result);
            result.AtualizarDados(pizzaDTO.Nome, pizzaDTO.Descricao, pizzaDTO.Preco, pizzaDTO.ImagemURL);
            _context.Update(result);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public void DadosDropDow()
        {
            var resp = new PostPizzaDropDown()
            {
                Sabores = _context.Sabores.OrderBy(x => x.Nome).ToList(),
                Tamanhos = _context.Tamanhos.OrderBy(x => x.Nome).ToList()
            };

            ViewBag.Sabores = new SelectList(resp.Sabores, "Id", "Nome");
            ViewBag.Tamanhos = new SelectList(resp.Tamanhos, "Id", "Nome");

        }
        public IActionResult Criar()
        {
            DadosDropDow();
            return View();
        }
        [HttpPost]
        public IActionResult Criar(PostPizzaDTO pizzaDTO)
        {
            Pizza Pizza = new Pizza
               (
                   pizzaDTO.Nome,
                   pizzaDTO.Descricao,
                   pizzaDTO.Preco,
                   pizzaDTO.ImagemURL,
                   pizzaDTO.TamanhoId
               );

            _context.Pizzas.Add(Pizza);
            _context.SaveChanges();

            foreach (var saborId in pizzaDTO.SaboresId)
            {
                var novosabor = new PizzasSabores(Pizza.Id,saborId);
                _context.PizzasSabores.Add(novosabor);
                _context.SaveChanges();
            }
            
            return RedirectToAction(nameof(Index));
         
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
