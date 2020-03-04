using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReceitasDeSucesso.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Mensagem"] = "Um simples site para cadastro de receitas.";
            ViewBag.Titulo = "Receitas de Sucesso!";
            return View("Home");
        }
    }
}
