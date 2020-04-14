using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using ReceitasDeSucesso.Models;

namespace ReceitasDeSucesso.ViewModels
{
    public class ReceitaViewModel
    {
        [Key]
        public Int64 ID { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Ingredientes { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string ModoDePreparo { get; set; }
        public string Tags { get; set; }
        public List<SelectListItem> listaDeCategorias { get; set; }
        [Required(ErrorMessage = "O campo categoria é obrigatório")]
        public int IdCategoria { get; set; }
        public string TituloCategoria { get; set; }
        public string DescricaoCategoria { get; set; }
        public IFormFile ImagemDaReceita { get; set; }

    }
}
