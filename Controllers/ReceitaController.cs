using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReceitasDeSucesso.Models;
using ReceitasDeSucesso.Client;

namespace ReceitasDeSucesso.Controllers
{
    public class ReceitaController : Controller
    {
        public IReceitaClient ReceitaClient;

        public ReceitaController(IReceitaClient receitaClient)
        {
            ReceitaClient = receitaClient;
        }

        public IActionResult Receita(Receita receita)
        {
            return View("ConsultaReceita",receita);
        }


        [HttpGet]
        public IActionResult CadastroReceita()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CadastroReceita(Receita receita)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                await ReceitaClient.InserirReceita(receita);

                TempData["message"] = "Salvo com sucesso!";

                return View(receita) ;

            }
            catch (Exception)
            {

                throw;
            }
        }
        
    }
}
