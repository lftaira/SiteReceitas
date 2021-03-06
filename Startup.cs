﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Config = builder.Build();

            var config = new AutoMapper.MapperConfiguration(cfg => 
            {
                cfg.CreateMap<ReceitaViewModel, Receita>();
                cfg.CreateMap<Receita, ReceitaViewModel>();
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
                    name: "CadastroReceita",
                    template: "{controller=Receita}/{action=Cadastrar}/");

                    routes.MapRoute(
                    name: "Consulta de Receitas",
                    template: "{controller=Receita}/{action=Listar}/");

                    routes.MapRoute(
                    name: "Editar Receita",
                    template:"{controller=Receita}/{action=Editar}/{id?}");

                    routes.MapRoute(
                    name: "Deletar Receita",
                    template:"{controller=Receita}/{action=Deletar}/{ID}");

                    routes.MapRoute(
                    name: "Cadastrar Categoria",
                    template:"{controller=Categoria}/{action=Cadastrar}/");
                }
                );
        }
    }
}