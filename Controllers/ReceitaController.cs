using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReceitasDeSucesso.Models;
using ReceitasDeSucesso.Service.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReceitasDeSucesso.ViewModels;
using AutoMapper;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace ReceitasDeSucesso.Controllers
{
    public class ReceitaController : Controller
    {
        public IReceitaService _receitaService;
        public ICategoriaService _categoriaService;

        public IMapper _mapper;

        public ReceitaController(IReceitaService receitaClient, ICategoriaService categoriaClient, IMapper mapper)
        {
            _receitaService = receitaClient;
            _categoriaService = categoriaClient;
            _mapper = mapper;
        }

        public IActionResult Receita(Receita receita)
        {
            return View("ConsultaReceita", receita);
        }


        [HttpGet]
        public async Task<IActionResult> CadastrarReceita()
        {
            IEnumerable<Categoria> listaCategoria = await ObterCategorias();
            ReceitaViewModel receitaVM = CriarVMePopularListaDeCategoria(listaCategoria);
            return View(receitaVM);
        }

        private async Task<IEnumerable<Categoria>> ObterCategorias()
        {
            return await _categoriaService.ObterListaCategoria();
        }

        private static ReceitaViewModel CriarVMePopularListaDeCategoria(IEnumerable<Categoria> listaCategoria, ReceitaViewModel receitaVM = null)
        {

            if (receitaVM == null)
                receitaVM = new ReceitaViewModel();

            receitaVM.listaDeCategorias = new List<SelectListItem>();

            foreach (var item in listaCategoria)
            {
                receitaVM.listaDeCategorias.Add(new SelectListItem
                {
                    Text = item.Descricao,
                    Value = item.Id.ToString(),
                    Selected = item.Id == receitaVM.IdCategoria
                });
            }
            return receitaVM;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarReceita(ReceitaViewModel receitaVm)
        {
            using (var memoryStream = new MemoryStream())
            using (var sw = new StreamWriter(memoryStream))
            using (var sr = new StreamReader(memoryStream))

            {
                await receitaVm.ImagemDaReceita.CopyToAsync(memoryStream);

                if (!ModelState.IsValid || memoryStream.Length > 2097152)
                {
                    ModelState.AddModelError("Imagem", "O arquivo é muito grande.");
                    return View(receitaVm);
                }

                Receita receita = await MapearVMeAtualizarCategoria(receitaVm);
                //var sw = new StreamWriter(memoryStream);
                sw.Flush();
                memoryStream.Position = 0;
                //var sr = new StreamReader(memoryStream);
                
                receita.Imagem = sr.ReadToEnd();
                if (await SalvarReceita(receita))
                    TempData["message"] = "Salvo com sucesso!";
                return View(receitaVm);
            }


        }

        private async Task<bool> SalvarReceita(Receita receita)
        {
            var httpResponde = await _receitaService.InserirItem(receita);
            return httpResponde.IsSuccessStatusCode;
        }

        private async Task<Receita> MapearVMeAtualizarCategoria(ReceitaViewModel receitaVm)
        {
            var receita = _mapper.Map<Receita>(receitaVm);
            receita.Categoria = await _categoriaService.ObterItem(receitaVm.IdCategoria);
            return receita;
        }

        [HttpGet]
        public async Task<IActionResult> ConsultarReceita()
        {
            var listaReceitas = await _receitaService.ObterLista();
            return View(listaReceitas);
        }

        [HttpGet]
        public async Task<IActionResult> EditarReceita(int? ID)
        {
            if (ID == null)
                return NotFound();

            var receita = await _receitaService.ObterItem(ID);

            var receitaVM = _mapper.Map<ReceitaViewModel>(receita);
            receitaVM.IdCategoria = receita.Categoria.Id;
            receitaVM.DescricaoCategoria = receita.Categoria.Descricao;
            receitaVM.TituloCategoria = receita.Categoria.Titulo;

            return View("EditarReceita", CriarVMePopularListaDeCategoria(await _categoriaService.ObterListaCategoria(), receitaVM));
        }

        [HttpPost]
        public async Task<IActionResult> EditarReceita(ReceitaViewModel receitaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Receita receita = await MapearVMeAtualizarCategoria(receitaViewModel);

            var httpResponse = await _receitaService.AlterarItem(receita.ID, receita);
            TempData["message"] = (httpResponse.IsSuccessStatusCode) ? "Salvo com sucesso!" : "Erro ao salvar. Tente novamente mais tarde.";
            return View(receitaViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> DeletarReceita(int? ID)
        {
            if (ID == null)
                return NotFound();

            var httResponse = await _receitaService.DeletarItem(ID);

            return Redirect("/receita/ConsultarReceita");
        }
    }
}
