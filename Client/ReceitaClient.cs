using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ReceitasDeSucesso.Models;

namespace ReceitasDeSucesso.Client
{
    public class ReceitaClient : IReceitaClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration configuration;

        public ReceitaClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
        }

        public Task<Receita> AlterarReceita(int id, Receita receita)
        {
            throw new NotImplementedException();
        }

        public Task<Receita> DeletarReceita(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Receita> InserirReceita(Receita receita)
        {
            var receitaJson =  JsonConvert.SerializeObject(receita);
            var httpResponse = await _httpClient.PostAsync(_httpClient.BaseAddress.AbsoluteUri,
                                                            new StringContent(receitaJson, encoding: default, "application/json")) ;

            return JsonConvert.DeserializeObject<Receita>(await httpResponse.Content.ReadAsStringAsync());
        }

        public Task<List<Receita>> ObterListaReceita()
        {
            throw new NotImplementedException();
        }

        public Task<Receita> ObterReceita(int id)
        {
            throw new NotImplementedException();
        }
    }
}
