using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ReceitasDeSucesso.Models;

namespace ReceitasDeSucesso.Service.Interface
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> ObterListaCategoria();
        Task<Categoria> ObterItem(int? id);
        Task<HttpResponseMessage> InserirItem(Categoria item);
        Task<HttpResponseMessage> AlterarItem(int id, Categoria item);
        Task<HttpResponseMessage> DeletarItem(int? id);
    }
}