using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReceitasDeSucesso.Service.Interface;
using ReceitasDeSucesso.Service.Implementacao;
using ReceitasDeSucesso.ViewModels;
using ReceitasDeSucesso.Models;
using AutoMapper;

namespace ReceitasDeSucesso
{
    public class Startup
    {

        private IConfigurationRoot Config;
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false);

            CriarServices(services);

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Config = builder.Build();

            var config = new AutoMapper.MapperConfiguration(cfg => 
            {
                cfg.CreateMap<ReceitaViewModel, Receita>();
                cfg.CreateMap<Receita, ReceitaViewModel>();
                cfg.CreateMap<IFormFile, byte[]>();
                cfg.CreateMap<byte[], IFormFile>();
            });

            IMapper mapper = config.CreateMapper();

            services.AddSingleton(mapper);
        }

        private void CriarServices(IServiceCollection services)
        {
            services.AddHttpClient<IReceitaService, ReceitaService>(client =>
            {
                client.BaseAddress = new Uri(Config["URLAPI_Receita"]);
            });

            services.AddHttpClient<ICategoriaService, CategoriaService>(client =>
            {
                client.BaseAddress = new Uri(Config["URLAPI_Categoria"]);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.EnvironmentName.Equals("Development"))
            {
                app.UseDeveloperExceptionPage();
                
            }

            app.UseStaticFiles();
            app.UseMvc(
                routes =>
                {
                    routes.MapRoute(
                    name: "RotaHome",
                    template: "{controller=Home}/{action=Index}/");

                    routes.MapRoute(
                    name: "RotaReceita",
                    template: "{controller=Receita}/{action=Index}/");

                    routes.MapRoute(
                    name: "CadastroReceita",
                    template: "{controller=Receita}/{action=CadastrarReceita}/");

                    routes.MapRoute(
                    name: "Consulta de Receitas",
                    template: "{controller=Receita}/{action=ListarReceita}/");

                    routes.MapRoute(
                    name: "Editar Receita",
                    template:"{controller=Receita}/{action=EditarReceita}/{id?}");

                    routes.MapRoute(
                    name: "Cadastrar Categoria",
                    template:"{controller=Categoria}/{action=CadastrarCategoria}/");

                    routes.MapRoute(
                    name: "Deletar Receita",
                    template:"{controller=Receita}/{action=DeletarReceita}/{ID}");
                }
                );
        }
    }
}