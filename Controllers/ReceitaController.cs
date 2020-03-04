using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReceitasDeSucesso.Models;

namespace ReceitasDeSucesso.Controllers
{
    public class ReceitaController : Controller
    {
        public IActionResult Receita()
        {
            var receita = new Receita();
            receita.Descricao = "teste";

            return View(receita);
        }

        public IActionResult CadastroReceita()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CadastroReceita(Receita receita)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return View(receita);
                }
                return View(receita);

            }
            catch (Exception)
            {

                throw;
            }
        }
        
    }
}
