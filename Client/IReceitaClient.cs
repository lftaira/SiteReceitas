using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReceitasDeSucesso.Models;

namespace ReceitasDeSucesso.Client
{
    public interface IReceitaClient
    {
        Task<List<Receita>> ObterListaReceita();
        Task<Receita> ObterReceita(int id);
        Task<Receita> InserirReceita(Receita receita);
        Task<Receita> AlterarReceita(int id, Receita receita);
        Task<Receita> DeletarReceita(int id);
    }
}
