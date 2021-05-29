using LanchesDev.Models;
using LanchesDev.Repositories;
using LanchesDev.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanchesDev.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public LancheController(ILancheRepository lancheRepository, ICategoriaRepository categoriaRepository)
        {
            _lancheRepository = lancheRepository;

            _categoriaRepository = categoriaRepository;
        }

        public IActionResult List(string categoria)
        {
            string _categoria = categoria;
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepository.Lanches.OrderBy(p => p.LancheId);
                categoriaAtual = "Todos os lanches";
            }
            else
            {
                //código otimizado
                lanches = _lancheRepository.Lanches.Where(l => l.Categoria.CategoriaNome.Equals(categoria))
                    .OrderByDescending(l => l.LancheId);

                categoriaAtual = _categoria;
            }

            var lancheListViewModel = new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };

            return View(lancheListViewModel);
        }

        public IActionResult Details(int lancheId)
        {
            var lanche = _lancheRepository.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
            if(lanche == null)
            {
                return View("^/Views/Error/Error.cshtml");
            }
            return View(lanche);
        }
    }
}
