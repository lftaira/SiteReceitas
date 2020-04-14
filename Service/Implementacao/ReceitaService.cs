using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ReceitasDeSucesso.Models;
using ReceitasDeSucesso.Service.Interface;

namespace ReceitasDeSucesso.Service.Implementacao
{
    public class ReceitaService : IReceitaService
    {
        private readonly HttpClient _httpClient;

        public ReceitaService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }
        public async Task<HttpResponseMessage> AlterarItem(int id, Receita receita)
        {
            var httpResponse = await _httpClient.GetAsync(string.Format("{0}/{1}",
                                                _httpClient.BaseAddress.AbsoluteUri, id.ToString()));
            if (!httpResponse.IsSuccessStatusCode)
                return httpResponse;

            return await _httpClient.PutAsync(_httpClient.BaseAddress.AbsoluteUri,
                                        new StringContent(await httpResponse.Content.ReadAsStringAsync(), encoding: default, "application/json"));
        }
        public async Task<HttpResponseMessage> DeletarItem(int? id)
        {
            var receitaDeleted = await _httpClient.DeleteAsync(string.Format("{0}/{1}",
                                _httpClient.BaseAddress.AbsoluteUri, id.ToString()));
            return receitaDeleted;
        }
        public async Task<HttpResponseMessage> InserirItem(Receita item)
        {
            {
                var receitaJson = JsonConvert.SerializeObject(item);
                return await _httpClient.PostAsync(_httpClient.BaseAddress.AbsoluteUri,
                                            new StringContent(receitaJson, encoding: default, "application/json"));
            }
        }
        public async Task<Receita> ObterItem(int? id)
        {
            Receita receita = null;
            var httpResponse = await _httpClient.GetAsync(string.Format("{0}/{1}",
                                                            _httpClient.BaseAddress.AbsoluteUri, id.ToString()));
            if (httpResponse.IsSuccessStatusCode)
                receita = JsonConvert.DeserializeObject<Receita>(await httpResponse.Content.ReadAsStringAsync());

            return receita;
        }

        public async Task<IEnumerable<Receita>> ObterLista()
        {
            var httpResponse = await _httpClient.GetAsync(_httpClient.BaseAddress.AbsoluteUri);

            return JsonConvert.DeserializeObject<IEnumerable<Receita>>(await httpResponse.Content.ReadAsStringAsync());
        }
    }
}
