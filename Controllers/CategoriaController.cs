using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReceitasDeSucesso.Models;
using ReceitasDeSucesso.Service.Interface;
using ReceitasDeSucesso.ViewModels;

namespace SiteReceitas.Controllers
{
    public class CategoriaController : Controller
    {
        ICategoriaService _categoriaService;
        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<IActionResult> CadastrarCategoria()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarCategoria(Categoria categoria)
        {
            var httpResponse = await _categoriaService.InserirItem(categoria);

            TempData["message"] = (httpResponse.IsSuccessStatusCode) ? 
                "Salvo com sucesso!" : "Erro ao salvar. " + httpResponse.ToString(); 

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RetornarCategoria()
        {
            var listaCategoria = await _categoriaService.ObterListaCategoria();
            return View(listaCategoria);
        }

        [HttpPut]
        public async Task<IActionResult> AlterarCategoria(int id, Categoria categoria)
        {
            var httpResponse = await _categoriaService.AlterarItem(id, categoria);
            return View();
        }
    }
}