using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ReceitasDeSucesso.Models;
using ReceitasDeSucesso.Service.Interface;

namespace ReceitasDeSucesso.Service.Implementacao
{
    public class CategoriaService : ICategoriaService
    {
        HttpClient _httpClient { get; set; }
        public CategoriaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<HttpResponseMessage> AlterarItem(int id, Categoria item)
        {
            var httpResponse = await _httpClient.GetAsync(string.Format("{0}/{1}",
                                                    _httpClient.BaseAddress.AbsoluteUri, id.ToString()));
            if (!httpResponse.IsSuccessStatusCode)
                return httpResponse;

            return await _httpClient.PostAsync(_httpClient.BaseAddress.AbsoluteUri,
                                                new StringContent(await httpResponse.Content.ReadAsStringAsync(),
                                                                 encoding: default, "application/json"));
        }

        public async Task<HttpResponseMessage> DeletarItem(int? id)
        {
            var httpResponse = await _httpClient.DeleteAsync(string.Format("{0}/{1}",
                                                    _httpClient.BaseAddress.AbsoluteUri, id.ToString()));
            return httpResponse;
        }

        public async Task<HttpResponseMessage> InserirItem(Categoria categoria)
        {
            var categoriaJson = JsonConvert.SerializeObject(categoria);
            return await _httpClient.PostAsync(_httpClient.BaseAddress.AbsoluteUri,
                                            new StringContent(categoriaJson,
                                            encoding: default, "application/json"));
        }

        public async Task<Categoria> ObterItem(int? id)
        {
            Categoria categoria = null;
            var httpResponse = await _httpClient.GetAsync(string.Format("{0}/{1}",
                                                    _httpClient.BaseAddress.AbsoluteUri, id.ToString()));
            if (httpResponse.IsSuccessStatusCode)
                categoria = JsonConvert.DeserializeObject<Categoria>(await httpResponse.Content.ReadAsStringAsync());

            return categoria;
        }

        public async Task<IEnumerable<Categoria>> ObterListaCategoria()
        {
            var httpResponse = await _httpClient.GetAsync(_httpClient.BaseAddress.AbsoluteUri);
            return JsonConvert.DeserializeObject<IEnumerable<Categoria>>(await httpResponse.Content.ReadAsStringAsync());
        }
    }
}