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
using System.Text;
using Microsoft.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Hosting;

namespace ReceitasDeSucesso.Controllers
{
    public class ReceitaController : Controller
    {
        public IReceitaService _receitaService;
        public ICategoriaService _categoriaService;
        public IWebHostEnvironment _webHostEnvironment;
        public IMapper _mapper;
        const string semImagem = "semimagem.jpeg";
        const string defaultPathImgs = "~/imgs/";

        public ReceitaController(IReceitaService receitaClient, ICategoriaService categoriaClient, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _receitaService = receitaClient;
            _categoriaService = categoriaClient;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Receita(Receita receita)
        {
            return View("ConsultaReceita", receita);
        }
        
        [HttpGet]
        public async Task<IActionResult> Cadastrar()
        {
            IEnumerable<Categoria> listaCategoria = await _categoriaService.ObterListaCategoria();
            ReceitaViewModel receitaVM = CriarVMePopularListaDeCategoria(listaCategoria);
            return View(receitaVM);
        }
        private static ReceitaViewModel CriarVMePopularListaDeCategoria(IEnumerable<Categoria> listaCategoria, 
                                                                        ReceitaViewModel receitaVM = null)
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
        public async Task<IActionResult> Cadastrar(ReceitaViewModel receitaVm)
        {
            if (!ModelState.IsValid)
            {
                return View(receitaVm);
            }

            Receita receita = await MapearVMeAtualizarCategoria(receitaVm);
            receita.Imagem = SalvarImagemERetornarGUID(receitaVm);

            if (await SalvarReceita(receita))
                TempData["message"] = "Salvo com sucesso!";
            return View(receitaVm);
        }

        private string SalvarImagemERetornarGUID(ReceitaViewModel receita)
        {
            string guidImage = null;
            //2097152
            if (receita.ImagemDaReceita != null)
            {
                var pathImagem = Path.Combine(_webHostEnvironment.WebRootPath, "imgs");
                guidImage = Guid.NewGuid().ToString() + "_" + receita.ImagemDaReceita.FileName.ToString();
                pathImagem = Path.Combine(pathImagem, guidImage);
                using (var fileStream = new FileStream(pathImagem, FileMode.Create))
                {
                     receita.ImagemDaReceita.CopyTo(fileStream);
                }
            }
            return guidImage;
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
        public async Task<IActionResult> Listar()
        {
            var listaReceitas = await _receitaService.ObterLista();
            
            foreach(Receita receita in listaReceitas)
            {
                if(receita.Imagem == null)
                    receita.Imagem = semImagem;
            }

            return View(listaReceitas);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int? ID)
        {
            if (ID == null)
                return NotFound();

            var receita = await _receitaService.ObterItem(ID);

            var receitaVM = _mapper.Map<ReceitaViewModel>(receita);
            receitaVM.IdCategoria = receita.Categoria.Id;
            receitaVM.DescricaoCategoria = receita.Categoria.Descricao;
            receitaVM.TituloCategoria = receita.Categoria.Titulo;

            return View("Editar", CriarVMePopularListaDeCategoria(await _categoriaService.ObterListaCategoria(), receitaVM));
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ReceitaViewModel receitaViewModel)
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
        public async Task<IActionResult> Deletar(int? ID)
        {
            if (ID == null)
                return NotFound();
            var httResponse = await _receitaService.DeletarItem(ID);
            return Redirect("/receita/ListarReceita");
        }

        public async Task<IActionResult> Consultar(int ID)
        {
            var receita = await _receitaService.ObterItem(ID);
            if(receita == null)
                return NotFound();
            if (receita.Imagem == null )
                receita.Imagem = Path.Combine(defaultPathImgs, semImagem);

            return View(receita);
        }
    }
}
