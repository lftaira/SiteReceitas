using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ReceitasDeSucesso.Models;

namespace ReceitasDeSucesso.Service.Interface
{
    public interface IReceitaService
    {
        Task<IEnumerable<Receita>> ObterLista();
        Task<Receita> ObterItem(int? id);
        Task<HttpResponseMessage> InserirItem(Receita item);
        Task<HttpResponseMessage> AlterarItem(Int64 id, Receita item);
        Task<HttpResponseMessage> DeletarItem(int? id);
    }
}
