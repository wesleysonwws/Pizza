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

    
        public class TamanhosController : Controller
        {
            private PizzariaDbContext _context;

            public TamanhosController(PizzariaDbContext context)
            {
                _context = context;
            }

            public IActionResult Index()
            {
                return View(_context.Tamanhos);
            }

            public IActionResult Detalhes(int id)
            {
                var resultado = _context.Tamanhos
                    .Include(P => P.Pizzas)
                    .FirstOrDefault(tamanho => tamanho.Id == id);

                if (resultado == null)
                    return View("NotFound");

                return View(resultado);
            }

            public IActionResult Criar() => View();

            [HttpPost]
            public IActionResult Criar(PostTamanhoDTO tamanhoDTO)
            {
                if (!ModelState.IsValid)
                    return View(tamanhoDTO);

                Tamanho tamanho = new Tamanho(tamanhoDTO.Nome);
                _context.Tamanhos.Add(tamanho);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            public IActionResult Atualizar(int? id)
            {
                if (id == null)
                    return NotFound();

                var result = _context.Tamanhos.FirstOrDefault(p => p.Id == id);

                if (result == null)
                    return View();

                return View(result);
            }

            [HttpPost]
            public IActionResult Atualizar(int id, PostTamanhoDTO tamanhoDTO)
            {
                var tamanho = _context.Tamanhos.FirstOrDefault(a => a.Id == id);

                if (!ModelState.IsValid)
                    return View(tamanho);

                tamanho.AtualizarDados(tamanho.Nome);

                _context.Update(tamanho);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            public IActionResult Deletar(int id)
            {
                var result = _context.Tamanhos.FirstOrDefault(a => a.Id == id);

                if (result == null) return View();

                return View(result);
            }

            [HttpPost, ActionName("Deletar")]
            public IActionResult ConfirmarDeletar(int id)
            {
                var result = _context.Tamanhos.FirstOrDefault(a => a.Id == id);
                _context.Tamanhos.Remove(result);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
        }
    }